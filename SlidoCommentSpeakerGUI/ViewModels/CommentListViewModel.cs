using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoCommentSpeakerGUI.ViewModels
{
	class CommentListViewModel : ViewModelBase
	{
		private string sessionName = "初期値だよん（取得失敗してるかも？)";
		public string SessionName
		{
			get => sessionName;
			set
			{
				sessionName = value;
				NotifyPropertyChanged();
			}
		}

		public int SessionID
		{
			get; set;
		}

		private ObservableCollection<CommentTipViewModel> _comments = new();
		public ObservableCollection<CommentTipViewModel> Comments
		{
			get => _comments;

			set
			{
				_comments = value;
				NotifyPropertyChanged();
			}
		}
	}
}
