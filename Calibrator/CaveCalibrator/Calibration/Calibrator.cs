using CaveCalibrator.Cave.Protocol;
using CaveCalibrator.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CaveCalibrator.Calibration
{
    public class Calibrator : CaveEvents
    {
        private CalibratorView _view;
        private CaveConnection _connection;

        /* The current calibration data */
        public Package currentPackage;

        /* Cave status */
        private bool _showHelpers = false;
        private bool _lockCameras = false;
        private int _onlyCameraDisplay = -1;

        /* Selected calibration */
        private int _selectedCalibrationIndex = 0;
        private QuadCorner _selectedCorner = QuadCorner.TopLeft;

        public Calibrator(CalibratorView view)
        {
            this._view = view;
            this._connection = new CaveConnection(Cave.Protocol.CaveProtocol.localhost, Cave.Protocol.CaveProtocol.port, this);
        }

        public bool Initialize()
        {
            if (!_connection.Connect())
            {
                return false;
            }
            // Should we load defaults here just as the original implementation does? I don't see why we should...?
            return true;
        }

        public void LoadCalibrationFromFile(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Debug.WriteLine("Loading calibration from file " + file);
                var json = File.ReadAllText(file);
                this.currentPackage = JsonConvert.DeserializeObject<Cave.Protocol.Package>(json);
                _connection.SendPackage(currentPackage);
            }
        }

        public void SaveCalibrationToFile(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Debug.WriteLine("Saving calibration to file " + file);
                var serialized = JsonConvert.SerializeObject(this.currentPackage, Formatting.Indented);
                File.WriteAllText(file, serialized);
            }
        }


        public void NextCalibration(int offset)
        {
            _selectedCalibrationIndex = MathUtil.Mod(_selectedCalibrationIndex + offset, currentPackage.calibrations.Length);
            RefreshSelectedCalibrationToView();
            RefreshOnlyCameraDisplay();
            _lastSwitchedIndex = -1;
        }

        public void SelectCorner(QuadCorner corner)
        {
            _selectedCorner = corner;
            _view.RefreshSelectedCorner(_selectedCorner);
        }

        public void OnCaveCalibrationMessage(Package calibration)
        {
            Debug.WriteLine("Recieved new calibration package: " + JsonConvert.SerializeObject(calibration, Formatting.Indented));
            currentPackage = calibration;
            RefreshSelectedCalibrationToView();
        }

        public void OnCaveShowHelpers(bool state)
        {
            Debug.WriteLine("Recieved show helpers: " + state);
            if (_showHelpers == state) return;
            _showHelpers = state;
            _view.RefreshView(_onlyCameraDisplay >= 0, _showHelpers);
        }

        public void OnCaveOnlyCameraDisplay(int display)
        {
            Debug.WriteLine("Recieved only camera display: " + display);
            if (_onlyCameraDisplay == display) return;
            _onlyCameraDisplay = display;
            _view.RefreshView(_onlyCameraDisplay >= 0, _showHelpers);
        }

        public void OnCaveLockCameras(bool state)
        {
            Debug.WriteLine("Recieved lock cameras: " + state);
            if (_lockCameras == state) return;
            _lockCameras = state;
        }

        public void SetHelpers(bool value)
        {
            if (_showHelpers != value)
            {
                _connection.SendShowHelpers(value);
            }
        }

        public void ToggleHelpers()
        {
            SetHelpers(!_showHelpers);
        }

        public void SetTurnOthersOff(bool state)
        {
            var targetDisplay = state ? currentPackage.calibrations[_selectedCalibrationIndex].virtualDisplay : -1;
            if (_onlyCameraDisplay != targetDisplay)
            {
                _connection.SendSingleRenderDisplay(targetDisplay);
            }
        }

        public void ToggleTurnOthersOff()
        {
            SetTurnOthersOff(_onlyCameraDisplay < 0);
        }

        public Cave.Protocol.Calibration GetSelectedCalibration()
        {
            return currentPackage.calibrations[_selectedCalibrationIndex % currentPackage.calibrations.Length];
        }

        public void SetSelectedCalibration(Cave.Protocol.Calibration calibration)
        {
            currentPackage.calibrations[_selectedCalibrationIndex % currentPackage.calibrations.Length] = calibration;
        }

        private void RefreshSelectedCalibrationToView()
        {
            _view.RefreshSelectedCalibration(GetSelectedCalibration(), currentPackage.calibrations.Length);
        }

        public void OnExit()
        {
            SetHelpers(false);
            SetTurnOthersOff(false);
        }

        bool updateEnqueued = false;

        public async void MoveSelectedCorner(double x, double y)
        {
            var calibration = GetSelectedCalibration();
            var quad = calibration.projectionQuad;
            var corner = quad.GetCorner(_selectedCorner);
            const float barrier = 3; // Absolute max for x/y
            corner.x = Math.Max(-barrier, Math.Min(barrier, corner.x + (float)x));
            corner.y = Math.Max(-barrier, Math.Min(barrier, corner.y + (float)y));
            quad.SetCorner(_selectedCorner, corner);
            calibration.projectionQuad = quad;
            calibration.projectionCorrection = true;
            SetSelectedCalibration(calibration);

            // Delay sending to aggregate multiple updates into one
            if (updateEnqueued)
            {
                return;
            }
            updateEnqueued = true;
            await Task.Delay(50);
            updateEnqueued = false;
            _connection.SendPackage(currentPackage);
        }

        private int _lastSwitchedIndex = -1;

        public void SetVirtualDisplay(int value)
        {
            var calibrations = currentPackage.calibrations;
            var selected = calibrations[_selectedCalibrationIndex];
            var prev = selected.virtualDisplay;
            if (prev == value)
            {
                return;
            }

            // Workaround for shuffle bug
            if (_lastSwitchedIndex > -1)
            {
                var last = calibrations[_lastSwitchedIndex];
                last.virtualDisplay = prev;
                calibrations[_lastSwitchedIndex] = last;
                _lastSwitchedIndex = 0;
            }

            // We may need to resend the single camera display as it changed
            var keepSingleCamera = _onlyCameraDisplay > -1;

            // We don't want to have multiple calibrations with the same virtual display target
            int otherIndex = -1;
            for (int i = 0; i < calibrations.Length; i++)
            {
                var calibration = calibrations[i];
                if (calibration.virtualDisplay == value)
                {
                    otherIndex = i;
                    break;
                }
            }
            if (otherIndex > -1)
            {
                var other = calibrations[otherIndex];
                other.virtualDisplay = prev;
                calibrations[otherIndex] = other;
                _lastSwitchedIndex = otherIndex;
            }

            selected.virtualDisplay = value;
            calibrations[_selectedCalibrationIndex] = selected;

            currentPackage.calibrations = calibrations;
            _connection.SendPackage(currentPackage);

            if (keepSingleCamera) SetTurnOthersOff(true);
        }

        private void RefreshOnlyCameraDisplay()
        {
            if (_onlyCameraDisplay > -1)
            {
                SetTurnOthersOff(true);
            }
        }

        public void OnCaveDisconnected()
        {
            var temp = currentPackage;

            // Keep reconnecting...
            while (!_connection.IsConnected)
            {
                Debug.WriteLine("Connection to cave lost, reconnecting...");
                Thread.Sleep(5000);
                this._connection = new CaveConnection(Cave.Protocol.CaveProtocol.localhost, Cave.Protocol.CaveProtocol.port, this);
                _connection.Connect();
            }

            _connection.SendPackage(temp);
        }
    }
}
