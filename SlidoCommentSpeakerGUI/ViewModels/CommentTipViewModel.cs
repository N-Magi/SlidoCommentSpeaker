using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SlidoCommentSpeakerGUI
{
	class CommentTipViewModel : INotifyPropertyChanged
	{

		private string _comment = string.Empty;
		public string Comment
		{
			get { return _comment; }
			set
			{
				_comment = value;
				NotifyPropertyChanged();
			}

		}
		private string _author = string.Empty;
		public string Author
		{
			get { return _author; }
			set
			{
				_author = value;
				NotifyPropertyChanged();
			}

		}

		private Brush _statusColour = Brushes.Transparent;
		public Brush StatusColour
		{
			get { return _statusColour; }
			set
			{
				_statusColour = value;
				NotifyPropertyChanged();
			}

		}

		public void setCommentReaded()
		{
			StatusColour = Brushes.Green;
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
