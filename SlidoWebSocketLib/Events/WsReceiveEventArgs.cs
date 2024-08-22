using SlidoWebSocketLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Events
{
	public class WsReceiveEventArgs : EventArgs
	{
		public SlidoMessage SlidoMessage { get; set; } 
		public ClientWebSocket? WsSocket { get; set; }
	}
}
