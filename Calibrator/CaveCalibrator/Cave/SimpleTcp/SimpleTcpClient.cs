using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Htw.Cave.SimpleTcp
{
    public class SimpleTcpClient : SimpleTcpEndpoint
    {
        private TcpClient m_Client;

        private Thread m_Thread;

        private NetworkStream m_ClientStream;

        private volatile bool m_Listen;

        private Action<SimpleTcpMessage> _onMessage;

        public SimpleTcpClient(Action<SimpleTcpMessage> onMessage) : base()
        {
            this._onMessage = onMessage;
        }

        public SimpleTcpClient() : base()
        {
            this._onMessage = null;
        }

        public bool Connect(string host, int port)
        {
            try
            {
                this.m_Client = new TcpClient();
                this.m_Client.Connect(host, port);
                this.m_ClientStream = this.m_Client.GetStream();
            }
            catch (SocketException)
            {
                this.m_Client = null;
                return false;
            }

            this.m_Listen = true;
            this.m_Thread = new Thread(ListenToServer);
            this.m_Thread.IsBackground = true;
            this.m_Thread.Start();

            return true;
        }

        public bool IsConnected() => this.m_Client != null;

        public void Stop()
        {
            this.m_Listen = false;

            if (this.m_Thread != null)
            {
                this.m_Thread.Join();
                this.m_Thread = null;
            }
        }

        public override bool SendMessage(SimpleTcpMessage message)
        {
            if (this.m_ClientStream == null)
                return false;

            this.m_ClientStream.SendMessage(message.type, message.bytes);

            return true;
        }

        private void ListenToServer()
        {
            while (this.m_Listen)
            {
                try
                {
                    var status = this.m_Client.GetClientStatus();

                    if (status == TcpClientStatus.Disconnect)
                        return;

                    if (status == TcpClientStatus.DataAvailable)
                    {
                        var type = this.m_ClientStream.ReadMessage(out byte[] bytes);
                        var message = new SimpleTcpMessage(type, bytes);
                        if (_onMessage == null)
                        {
                            base.messageQueue.Enqueue(message);
                        }
                        else
                        {
                            _onMessage.Invoke(message);
                        }

                    }
                    else
                    {
                        Thread.Sleep(20);
                    }
                }
                catch (IOException)
                {
                    this.m_Listen = false;
                }
            }

            this.m_ClientStream.Close();
            this.m_ClientStream = null;
            this.m_Client.Close();
            this.m_Client = null;
        }
    }
}
