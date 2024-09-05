using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoCommentSpeakerGUI.ViewModels
{
	class MenuTipViewModel : ViewModelBase
	{
		private string menuHeader = "";
		public string MenuHeader
		{
			get => menuHeader;
			set
			{
				menuHeader = value;
				NotifyPropertyChanged();
			}

		}

		private bool menuChecked = false;
		public bool MenuChecked
		{
			get => menuChecked;
			set
			{
				menuChecked = value;
				NotifyPropertyChanged();
			}

		}

		public event EventHandler onMenuActivated;
		public event EventHandler onMenuDeactivated;
		public RelayCommand onMenuClicked { get; set; }

		public MenuTipViewModel()
		{
			onMenuClicked = new RelayCommand(_ =>
			{
				//チェックをフリップ
				MenuChecked = !MenuChecked;
				Console.WriteLine("hello");
				//有効化時の処理
				if (menuChecked)
				{
					onMenuActivated?.Invoke(this, EventArgs.Empty);
				}
				else
				{
					onMenuDeactivated?.Invoke(this, EventArgs.Empty);
				}
			});
		}



	}
}
