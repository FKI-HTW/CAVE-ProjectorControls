using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Htw.Cave.SimpleTcp
{
	public abstract class SimpleTcpEndpoint
	{
		public ConcurrentQueue<SimpleTcpMessage> messageQueue;
		
		public SimpleTcpEndpoint()
		{
			this.messageQueue = new ConcurrentQueue<SimpleTcpMessage>();
		}
		
		public abstract bool SendMessage(SimpleTcpMessage message);
	}
}
