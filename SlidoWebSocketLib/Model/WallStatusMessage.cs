using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model
{

	public class WallStatusMessage
	{
		public int event_id { get; set; }
		public int event_section_id { get; set; }
		public string event_section_uuid { get; set; }
		public bool poll_deactivated { get; set; }
		public bool idea_topic_deactivated { get; set; }
		public Statuses statuses { get; set; }
	}

	public class Statuses
	{
		public bool questions { get; set; }
		public bool ideas { get; set; }
		public bool poll { get; set; }
		public bool leaderboard { get; set; }
		public bool twitter { get; set; }
		public bool presentation { get; set; }
	}

}
