using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveCalibrator.Cave.Protocol
{
    internal interface CaveEvents
    {
        void OnCaveCalibrationMessage(Package calibration);
        void OnCaveShowHelpers(bool state);

        void OnCaveLockCameras(bool state);

        void OnCaveOnlyCameraDisplay(int display);

        void OnCaveDisconnected();
    }
}
