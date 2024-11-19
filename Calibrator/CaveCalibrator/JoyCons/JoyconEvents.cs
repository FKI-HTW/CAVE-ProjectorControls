using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveCalibrator.JoyCons
{
    public interface JoyconEvents
    {
        void OnDPadUp();
        void OnDPadDown();
        void OnDPadLeft();
        void OnDPadRight();
        void OnStickDragged(float x, float y, bool pressed);
        void OnJoyconConnectionChange(bool searchForJoycons, bool leftConnected, bool rightConnected);
        void OnShoulder(bool leftJoycon);
        void OnPlus();
        void OnMinus();
        void OnHome();
        void OnCapture();
    }
}
