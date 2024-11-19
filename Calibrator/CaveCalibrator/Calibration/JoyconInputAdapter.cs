using CaveCalibrator.Calibration;
using CaveCalibrator.Cave.Protocol;
using CaveCalibrator.JoyCons;
using CaveCalibrator.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveCalibrator.Cave
{
    internal class JoyconInputAdapter : JoyconEvents
    {
        private static double DragSpeed = 0.0005; // Multiplier for the joycon stick when moving around
        private static double FastDragSpeed = 0.005; // Multiplier for the joycon stick when pressing and moving around

        private Calibrator _calibrator;
        private CalibratorView _view;

        private bool _right = false;
        private bool _down = false;

        public JoyconInputAdapter(Calibrator calibrator, CalibratorView view)
        {
            this._calibrator = calibrator;
            this._view = view;
        }

        public void OnDPadUp()
        {
            Debug.WriteLine("Up");
            _calibrator.SelectCorner(_right ? QuadCorner.TopRight : QuadCorner.TopLeft);
            _down = false;
        }

        public void OnDPadDown()
        {
            Debug.WriteLine("Down");
            _calibrator.SelectCorner(_right ? QuadCorner.BottomRight : QuadCorner.BottomLeft);
            _down = true;
        }

        public void OnDPadLeft()
        {
            Debug.WriteLine("Left");
            _calibrator.SelectCorner(_down ? QuadCorner.BottomLeft : QuadCorner.TopLeft);
            _right = false;
        }

        public void OnDPadRight()
        {
            Debug.WriteLine("Right");
            _calibrator.SelectCorner(_down ? QuadCorner.BottomRight : QuadCorner.TopRight);
            _right = true;
        }

        public void OnStickDragged(float x, float y, bool pressed)
        {
            Debug.WriteLine("Stick " + x + " | " + y + " pressed=" + pressed);
            var multiplier = pressed ? FastDragSpeed : DragSpeed;
            _calibrator.MoveSelectedCorner(x * multiplier, y * multiplier);
        }

        public void OnJoyconConnectionChange(bool search, bool leftConnected, bool rightConnected)
        {
            _view.OnJoyconConnectionChange(search, leftConnected, rightConnected);
        }

        public void OnShoulder(bool leftJoycon)
        {
            Debug.WriteLine("Next: " + leftJoycon);
            _calibrator.NextCalibration(leftJoycon ? -1 : 1);
        }

        public void OnPlus()
        {
            Debug.WriteLine("plus");
            var display = _calibrator.GetSelectedCalibration().virtualDisplay;
            var next = MathUtil.Mod(display + 1, _calibrator.currentPackage.calibrations.Length);
            _calibrator.SetVirtualDisplay(next);
        }

        public void OnMinus()
        {
            Debug.WriteLine("minus");
            var display = _calibrator.GetSelectedCalibration().virtualDisplay;
            var next = MathUtil.Mod(display - 1, _calibrator.currentPackage.calibrations.Length);
            _calibrator.SetVirtualDisplay(next);
        }

        public void OnHome()
        {
            Debug.WriteLine("home");
            _calibrator.ToggleTurnOthersOff();
        }

        public void OnCapture()
        {
            Debug.WriteLine("capture");
            _calibrator.ToggleHelpers();
        }
    }
}
