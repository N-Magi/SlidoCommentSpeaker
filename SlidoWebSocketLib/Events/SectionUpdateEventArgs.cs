using SlidoWebSocketLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Events
{
	public class SectionUpdateEventArgs : EventArgs
	{
		public SectionsUpdateMessage UpdateMessage { get; set; } = new SectionsUpdateMessage();
	}
}
