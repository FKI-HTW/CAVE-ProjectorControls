using CaveCalibrator.Cave.Protocol;
using Htw.Cave.SimpleTcp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaveCalibrator.Calibration
{
    internal class CaveConnection
    {
        private readonly string _host;
        private readonly int _port;
        private readonly SimpleTcpClient _client;
        private readonly CaveEvents _eventConsumer;

        private bool _connected;
        public bool IsConnected
        {
            get { return _connected; }
        }

        public CaveConnection(string host, int port, CaveEvents eventConsumer)
        {
            _host = host;
            _port = port;
            _client = new SimpleTcpClient(m => HandleMessage(m));
            _eventConsumer = eventConsumer;
        }

        public bool Connect()
        {
            Debug.WriteLine("Connecting to cave...");
            if (_client.Connect(_host, _port))
            {
                _connected = true;
                Sync();
                SendShowHelpers(true);
                Debug.WriteLine("Successfully connected to " + _host + ":" + _port);
            }
            else
            {
                _connected = false;
                Debug.WriteLine("Connection to " + _host + ":" + _port + " failed!");
            }
            return _connected;
        }

        public void Sync()
        {
            _client.SendMessage(new SimpleTcpMessage(Cave.Protocol.Message.Sync));
        }


        public void SendPackage(Package currentPackage)
        {
            var serialized = JsonConvert.SerializeObject(currentPackage, Formatting.Indented);
            _client.SendMessage(new SimpleTcpMessage(Cave.Protocol.Message.Calibration, serialized));
        }

        public void SendShowHelpers(bool value)
        {
            Debug.WriteLine("Send show helpers...");
            _client.SendMessage(new SimpleTcpMessage(Cave.Protocol.Message.ShowHelpers, value));
        }

        public void SendLockCameras(bool value)
        {
            _client.SendMessage(new SimpleTcpMessage(Cave.Protocol.Message.LockCameras, value));
        }

        public void SendSingleRenderDisplay(int display)
        {
            _client.SendMessage(new SimpleTcpMessage(Cave.Protocol.Message.OnlyCameraDisplay, display));
        }

        private void HandleMessage(SimpleTcpMessage message)
        {
            switch (message.type)
            {
                case Cave.Protocol.Message.Calibration:
                    _eventConsumer.OnCaveCalibrationMessage(JsonConvert.DeserializeObject<Package>(message.GetString()));
                    break;
                case Cave.Protocol.Message.ShowHelpers:
                    _eventConsumer.OnCaveShowHelpers(message.GetBool());
                    break;
                case Cave.Protocol.Message.LockCameras:
                    _eventConsumer.OnCaveLockCameras(message.GetBool());
                    break;
                case Cave.Protocol.Message.OnlyCameraDisplay:
                    _eventConsumer.OnCaveOnlyCameraDisplay(message.GetInt());
                    break;
                default:
                    _connected = false;
                    _eventConsumer.OnCaveDisconnected();
                    _client.Stop();
                    break;
            }
        }
    }
}
