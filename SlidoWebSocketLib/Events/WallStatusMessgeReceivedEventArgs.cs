using SlidoWebSocketLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Events
{
	public class WallStatusMessgeReceivedEventArgs : EventArgs
	{
		public WallStatusMessage WallStatusMessage { get; set; } = new WallStatusMessage();
	}
}
