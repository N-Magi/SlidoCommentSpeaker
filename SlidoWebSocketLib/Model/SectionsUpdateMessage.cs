using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model
{
	public class SectionsUpdateMessage
	{
		public int event_id { get; set; }
		public string uuid { get; set; }
		public Section[] sections { get; set; }
	}

	public class Section
	{
		public UpdateAttrs attrs { get; set; }
		public string uuid { get; set; }
		public int event_id { get; set; }
		public int event_section_id { get; set; }
		public int order { get; set; }
		public string name { get; set; }
		public bool is_active { get; set; }
		public bool is_deleted { get; set; }
		public DateTime date_created { get; set; }
		public DateTime date_updated { get; set; }
	}

	public class UpdateAttrs
	{
		public string color { get; set; }
		public Statusbar statusbar { get; set; }
	}

	public class Statusbar
	{
		public string wallScreen { get; set; }
		public object implicitSwitcherScreen { get; set; }
		public string previousWallScreen { get; set; }
	}

}
