using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveCalibrator.Calibration
{
    public interface CalibratorView
    {

        void CaveDisconnected();

        void RefreshSelectedCalibration(Cave.Protocol.Calibration selected, int total);

        void RefreshSelectedCorner(Cave.Protocol.QuadCorner selected);

        void RefreshView(bool othersOff, bool showHelpers);

        void OnJoyconConnectionChange(bool search, bool left, bool right);

    }
}
