using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static JoyCon.Joycon;

namespace CaveCalibrator.JoyCons
{
    public class JoyconConnector
    {
        private static int ActionDelay = 200; // The delay to block input after a button was pressed in ms - fixes repeated button presses detected on bad connection

        private JoyconEvents _events;
        private JoyCon.JoyconManager _joyconManager;
        private bool _running;

        private bool _scanForJoycons;

        private int _prevJoyconCount = 0;
        private long _lastAction = 0;

        public JoyconConnector(JoyconEvents events)
        {
            _events = events;
            // hidapi must be present in build directory
            _joyconManager = new JoyCon.JoyconManager();
        }

        public void Start()
        {
            _running = true;
            var scanningThread = new Thread(RunScanningLoop);
            var pollingThread = new Thread(RunPollingLoop);
            scanningThread.Start();
            pollingThread.Start();
        }

        private void RunPollingLoop()
        {
            Debug.WriteLine("Connecting to joycons...");
            while (_running)
            {
                foreach (var joycon in _joyconManager.JoyCons)
                {

                    Poll(joycon);
                }
                Thread.Sleep(10);
            }
        }


        private void Poll(JoyCon.Joycon joycon)
        {
            joycon.Update(TimeSpan.Zero);

            if (Environment.TickCount - _lastAction < ActionDelay)
            {
                return;
            }

            var buttons = Enum.GetValues(typeof(Button)).Cast<Button>();
            foreach (var button in buttons)
            {
                if (!joycon.GetButtonDown((Button)button)) continue;

                bool anyAction = true;
                switch (button)
                {
                    case Button.DPAD_UP: _events.OnDPadUp(); break;
                    case Button.DPAD_DOWN: _events.OnDPadDown(); break;
                    case Button.DPAD_LEFT: _events.OnDPadLeft(); break;
                    case Button.DPAD_RIGHT: _events.OnDPadRight(); break;
                    case Button.SHOULDER_1: _events.OnShoulder(joycon.isLeft); break;
                    case Button.PLUS: _events.OnPlus(); break;
                    case Button.MINUS: _events.OnMinus(); break;
                    case Button.HOME: _events.OnHome(); break;
                    case Button.CAPTURE: _events.OnCapture(); break;
                    default: anyAction = false; break;
                }
                if (anyAction)
                {
                    _lastAction = Environment.TickCount;
                    return;
                }
            }

            var stick = joycon.GetStick();
            if ((stick[0] != 0f && !double.IsInfinity(stick[0])) || (stick[1] != 0f && !double.IsInfinity(stick[0])))
            {
                _events.OnStickDragged(stick[0], stick[1], joycon.GetButton(JoyCon.Joycon.Button.STICK));
            }
        }

        private void RunScanningLoop()
        {
            while (_running)
            {
                Scan();
                Thread.Sleep(3000);
            }
        }

        internal void OnExit()
        {
            _running = false;
            Debug.WriteLine("Detaching joycons...");
            _joyconManager.Dispose();
        }

        private void Scan()
        {
            if (!_scanForJoycons) return;

            // No need for more than 2 joycons
            if (_joyconManager.JoyCons.Count == 2)
            {
                return;
            }

            Debug.WriteLine("Searching joycons...");

            _joyconManager.RefreshJoyConList();
            var newJoyconCount = _joyconManager.JoyCons.Count;

            if (newJoyconCount != _prevJoyconCount)
            {
                _prevJoyconCount = newJoyconCount;
                Debug.WriteLine("Joycon count changed...");
                CallConnectionEvent();
            }
        }

        private void CallConnectionEvent()
        {
            bool left = false;
            bool right = false;
            foreach (var joycon in _joyconManager.JoyCons)
            {
                left |= joycon.isLeft;
                right |= !joycon.isLeft;
            }
            _events.OnJoyconConnectionChange(_scanForJoycons, left, right);
        }

        internal void ToggleScanForJoycons()
        {
            _scanForJoycons = !_scanForJoycons;
            CallConnectionEvent();
        }
    }
}
