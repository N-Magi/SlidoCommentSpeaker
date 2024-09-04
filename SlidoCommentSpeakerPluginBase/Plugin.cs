using System.Windows.Input;

namespace SlidoCommentSpeakerPluginBase
{
	public interface IPlugin:ICommand
	{
		string Name { get; }
		string Version { get; }

		string Description { get; }
		public void Init(IPluginContext context);
	}

	public interface IPluginContext
	{
		public delegate void OnCommentRecievedEventHandler(object sender, CommentRecievedEventArgs args);
		public event OnCommentRecievedEventHandler OnCommentRecieved;
	}

	public class CommentRecievedEventArgs
	{
		public string Comment { get; set; }
	}

}
