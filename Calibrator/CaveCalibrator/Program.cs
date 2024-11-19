using CaveCalibrator.Calibration;
using CaveCalibrator.Cave;
using CaveCalibrator.JoyCons;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaveCalibrator
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            CalibratorForm form = new CalibratorForm();
            Calibrator calibrator = new Calibrator(form);
            JoyconInputAdapter joyconInputAdapter = new JoyconInputAdapter(calibrator, form);
            JoyconConnector joycons = new JoyconConnector(joyconInputAdapter);
            form.Initialize(calibrator, joycons);

            if (calibrator.Initialize())
            {
                joycons.Start();
                Application.Run(form);
            }
            else
            {
                form.CaveDisconnected();
            }
        }
    }
}
