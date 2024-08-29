using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SlidoWebSocketLib;

namespace SlidoCommentSpeakerGUI
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		SlidoWebSocketLib.SlidoClient slidoClient = null;

		private readonly Dispatcher dispatcher;

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

		private string _slidoUrl = string.Empty;
		public string SlidoUrl
		{
			get => _slidoUrl;
			set
			{
				_slidoUrl = value;
				NotifyPropertyChanged();
			}
		}

		private string _buttonText = "Connect";
		public string ButtonText
		{
			get => _buttonText;
			set
			{
				_buttonText = value;
				NotifyPropertyChanged();
			}
		}

		private bool _slidoConnected = false;
		public bool SlidoConnected
		{
			get => _slidoConnected;
			set
			{
				_slidoConnected = value;
				NotifyPropertyChanged();
			}
		}


		public RelayCommand ConnectButtonPressed { get; set; }



		public MainWindowViewModel()
		{
			ConnectButtonPressed = new RelayCommand(_ => { onConnectButtonPressed(); });
			dispatcher = Dispatcher.CurrentDispatcher;
		}

		/// <summary>
		/// 接続・切断ボタン押下時の処理
		/// </summary>
		public async void onConnectButtonPressed()
		{
			if (SlidoUrl == string.Empty || SlidoUrl == null) return;

			if (slidoClient?.isConnected != null)
			{

				ButtonText = "Connect";
				await slidoClient.DisconnectAsync();
				slidoClient.Dispose();
				return;
			}

			var tokens = await SlidoClient.GetTargetAndToken(SlidoUrl, CancellationToken.None);
			slidoClient = new(tokens);

			slidoClient.onSlidoNewQuestionReceived += SlidoClient_onSlidoNewQuestionReceived;

			slidoClient.onSlidoConnected += (object args) =>
			{
				dispatcher?.BeginInvoke(new Action(() => { SlidoConnected = true; }));
			};
			slidoClient.onSlidoDisconnected += (object args) =>
			{
				dispatcher?.BeginInvoke(new Action(() => { SlidoConnected = false; }));
			};

			//Comments.Add(new CommentTipViewModel() { Author = "aaa", Comment = "bbb" });
			Console.WriteLine("Connected");

			ButtonText = "Disconnect";

			await slidoClient.Connect();


			//終了時謎の websocket Exceptionが発生している
		}

		private void SlidoClient_onSlidoNewQuestionReceived(object sender, SlidoWebSocketLib.Events.SlidoNewQuestionReceiveEventArgs args)
		{
			Console.WriteLine("onCommentRecieved");
			dispatcher.BeginInvoke(new Action(() => { Comments.Add(new CommentTipViewModel() { Author = args.QuestonMessage.author.name, Comment = args.QuestonMessage.text_formatted }); }));
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


	}
}
