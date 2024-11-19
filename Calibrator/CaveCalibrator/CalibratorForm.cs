using CaveCalibrator.Calibration;
using CaveCalibrator.Cave;
using CaveCalibrator.Cave.Protocol;
using CaveCalibrator.JoyCons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveCalibrator
{
    public partial class CalibratorForm : Form, CalibratorView
    {
        private Calibrator _calibrator;
        private JoyconConnector _joycons;

        private Point _draggingWindowOffset;
        private bool _draggingWindow;

        private Point _draggingQuadOffset;
        private bool _draggingQuad;

        private bool _closing;

        public void Initialize(Calibrator calibrator, JoyconConnector joycons)
        {
            _calibrator = calibrator;
            _joycons = joycons;
            InitializeComponent();
        }

        private void Quit()
        {
            this._closing = true;
            this._calibrator.OnExit();
            this._joycons.OnExit();
            Thread.Sleep(100); // Give it some time to gracefully disconnect
            Application.Exit();
        }

        public void CaveDisconnected()
        {
            MessageBox.Show("Verbindung zur HTW Cave nicht möglich.\nBitte sicherstellen, dass eine Unity-Application mit dem Cave-Package gestartet ist und erneut versuchen.", "Cave Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Quit();
        }

        public void RefreshSelectedCalibration(Cave.Protocol.Calibration selected, int total)
        {
            Invoke((MethodInvoker)delegate
            {
                SelectedDisplay.Text = selected.name;
                DisplayNumber.Value = selected.virtualDisplay;
                DisplayNumber.Maximum = total - 1; // First index is 0 -> so last is total - 1 
            });
        }
        public void RefreshSelectedCorner(QuadCorner selected)
        {
            // Pascal Case -> "Human Readable"
            var text = Regex.Replace(selected.ToString(), "([A-Z])", " $1").TrimStart();

            Invoke((MethodInvoker)delegate
            {
                SelectedCorner.Text = text;
            });
        }

        public void RefreshView(bool othersOff, bool showHelpers)
        {
            if (_closing) return;
            Invoke((MethodInvoker)delegate
            {
                Debug.WriteLine("Refresh view others off to " + othersOff);
                OthersOffCheckbox.Checked = othersOff;
                ShowQuadsCheckbox.Checked = showHelpers;
            });
        }

        public void OnJoyconConnectionChange(bool searching, bool leftConnected, bool rightConnected)
        {
            Invoke((MethodInvoker)delegate
            {
                JoyConLeft.BackColor = leftConnected ? Color.Green : searching ? Color.Yellow : Color.Red;
                JoyConRight.BackColor = rightConnected ? Color.Green : searching ? Color.Yellow : Color.Red;
            });
        }

        private void CloseApp_Click(object sender, EventArgs e)
        {
            Visible = false;
            Quit();
        }

        private void CalibratorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Visible = false;
            Quit();
        }


        /* Form input events */
        private void NextButton_Click(object sender, EventArgs e)
        {
            _calibrator.NextCalibration(1);
        }

        private void PrevButton_Click(object sender, EventArgs e)
        {
            _calibrator.NextCalibration(-1);
        }

        private void TopLeft_Click(object sender, EventArgs e)
        {
            _calibrator.SelectCorner(Cave.Protocol.QuadCorner.TopLeft);
        }

        private void TopRight_Click(object sender, EventArgs e)
        {
            _calibrator.SelectCorner(Cave.Protocol.QuadCorner.TopRight);
        }

        private void BottomLeft_Click(object sender, EventArgs e)
        {
            _calibrator.SelectCorner(Cave.Protocol.QuadCorner.BottomLeft);
        }

        private void BottomRight_Click(object sender, EventArgs e)
        {
            _calibrator.SelectCorner(Cave.Protocol.QuadCorner.BottomRight);
        }

        private void ShowQuadsCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _calibrator.SetHelpers(ShowQuadsCheckbox.Checked);
        }

        private void OthersOffCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _calibrator.SetTurnOthersOff(OthersOffCheckbox.Checked);
        }

        private void DisplayNumber_ValueChanged(object sender, EventArgs e)
        {
            _calibrator.SetVirtualDisplay((int)DisplayNumber.Value);
        }

        private void BrowseButton_Click_1(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _calibrator.LoadCalibrationFromFile(openFileDialog.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Datei konnte nicht geladen werden:\n" + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            var date = DateTime.Now;
            saveFileDialog.FileName = "calibration-" + date.ToString("yyyy-MM-dd") + ".json";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                _calibrator.SaveCalibrationToFile(saveFileDialog.FileName);
            }
        }
        private void HelpButton_Click(object sender, EventArgs e)
        {
            var help = new HelpForm();
            help.ShowDialog();
        }

        private void CalibrationQuad_MouseDown(object sender, MouseEventArgs e)
        {
            _draggingQuadOffset.X = e.X;
            _draggingQuadOffset.Y = e.Y;
            _draggingQuad = true;
        }

        private void CalibrationQuad_MouseUp(object sender, MouseEventArgs e)
        {
            _draggingQuad = false;
        }

        private void CalibrationQuad_MouseMove(object sender, MouseEventArgs e)
        {
            if (_draggingQuad)
            {
                var point = new Point(e.X - _draggingQuadOffset.X, e.Y - _draggingQuadOffset.Y);
                _draggingQuadOffset.X = e.X;
                _draggingQuadOffset.Y = e.Y;
                _calibrator.MoveSelectedCorner(point.X / 500f, point.Y / -500f);
            }
        }

        private void OnDraggerMouseDown(object sender, MouseEventArgs e)
        {
            _draggingWindowOffset.X = e.X;
            _draggingWindowOffset.Y = e.Y;
            _draggingWindow = true;
        }

        private void OnDraggerMouseUp(object sender, MouseEventArgs e)
        {
            _draggingWindow = false;
        }

        private void OnDraggerMouseMove(object sender, MouseEventArgs e)
        {
            if (_draggingWindow)
            {
                Point currentCursor = PointToScreen(e.Location);
                Location = new Point(currentCursor.X - _draggingWindowOffset.X, currentCursor.Y - _draggingWindowOffset.Y);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            _joycons.ToggleScanForJoycons();
        }
    }
}
