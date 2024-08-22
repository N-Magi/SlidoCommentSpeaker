using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model
{
	public class AuthApi
	{
		public string access_token { get; set; }
		public int event_id { get; set; }
		public int event_user_id { get; set; }
	}

}
