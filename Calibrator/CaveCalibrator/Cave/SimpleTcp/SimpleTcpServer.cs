using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Htw.Cave.SimpleTcp
{
	public class SimpleTcpServer : SimpleTcpEndpoint
	{		
		private TcpListener m_Listener;
		
		private Thread m_Thread;
		
		private volatile bool m_Listen;
		
		private NetworkStream m_ClientStream;
		
		public SimpleTcpServer() : base()
		{
		}
		
		public bool Listen(int port)
		{
			try
			{
				this.m_Listener = new TcpListener(IPAddress.Loopback, port);
				this.m_Listener.Start();
			} catch (SocketException) {
				return false;
			}
			
			this.m_Listen = true;
			this.m_Thread = new Thread(ListenIncomingConnections);
			this.m_Thread.IsBackground = true;
			this.m_Thread.Start();
			
			return true;
		}
		
		public void Stop()
		{
			this.m_Listen = false;
			
			if(this.m_Thread != null)
			{
				this.m_Thread.Join();
				this.m_Thread = null;
			}
			
			if(this.m_Listener != null)
			{
				this.m_Listener.Stop();
				this.m_Listener = null;
			}
		}
		
		public override bool SendMessage(SimpleTcpMessage message)
		{
			if(this.m_ClientStream == null)
				return false;
				
			this.m_ClientStream.SendMessage(message.type, message.bytes);
			
			return true;
		}
		
		private void ListenIncomingConnections()
		{
			while(this.m_Listen)
			{
				while(!this.m_Listener.Pending() && this.m_Listen)
					Thread.Sleep(20);
					
				if(!this.m_Listen)
					break;
			
				var client = this.m_Listener.AcceptTcpClient();
				
				this.m_ClientStream = client.GetStream();
								
				ListenToClient(client);
				
				this.m_ClientStream.Close();
				this.m_ClientStream = null;
				
				client.Close();
			}
		}
		
		private void ListenToClient(TcpClient client)
		{
			while(this.m_Listen)
			{
				try
				{
					var status = client.GetClientStatus();
					
					if(status == TcpClientStatus.Disconnect)
						return;
				
					if(status == TcpClientStatus.DataAvailable)
					{
						var type = this.m_ClientStream.ReadMessage(out byte[] bytes);
						base.messageQueue.Enqueue(new SimpleTcpMessage(type, bytes));
					} else {
						Thread.Sleep(20);
					}
				} catch(IOException) {
					return;
				}
			}
		}
	}
}
