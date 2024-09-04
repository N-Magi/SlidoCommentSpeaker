using SlidoCommentSpeakerPluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoCommentSpeakerGUI.Model
{
	internal class PluginContext : IPluginContext
	{
		public event IPluginContext.OnCommentRecievedEventHandler OnCommentRecieved;

		public void InvokeOnCommentRecived(string comment)
		{
			OnCommentRecieved.Invoke(this, new CommentRecievedEventArgs() { Comment = comment });
		}

	}
}
