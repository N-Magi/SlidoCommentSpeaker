using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.DirectoryServices;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using SlidoCommentSpeakerGUI.ViewModels;
using SlidoWebSocketLib;
using VoicevoxRestLib;

namespace SlidoCommentSpeakerGUI
{
	class MainWindowViewModel : ViewModelBase
	{
		SlidoWebSocketLib.SlidoClient slidoClient = null;

		private readonly Dispatcher dispatcher;

		private ObservableCollection<CommentListViewModel> _comments = new();
		public ObservableCollection<CommentListViewModel> Comments
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

		private bool _voicevoxEnabled = false;
		public bool VoicevoxEnabled
		{
			get => _voicevoxEnabled;
			set
			{
				_voicevoxEnabled = value;
				NotifyPropertyChanged();
			}
		}

		private VoicevoxClient voiceClient = new VoicevoxClient();

		public RelayCommand ConnectButtonPressed { get; set; }



		public MainWindowViewModel()
		{
			ConnectButtonPressed = new RelayCommand(_ => { onConnectButtonPressed(); });
			//PropertyChanged += MainWindowViewModel_PropertyChanged;
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

				Comments.Clear();
				ButtonText = "Connect";
				await slidoClient.DisconnectAsync();
				slidoClient.Dispose();
				return;
			}


			slidoClient = new();
			var tokens = await slidoClient.GetTargetAndToken(SlidoUrl, CancellationToken.None);

			slidoClient.SetTokens(tokens);

			foreach (var section in slidoClient.Summary.sections)
			{
				var commentListVM = new CommentListViewModel();
				commentListVM.SessionName = section.name;
				commentListVM.SessionID = section.event_section_id;
				Comments.Add(commentListVM);
			}

			slidoClient.onSlidoNewQuestionReceived += SlidoClient_onSlidoNewQuestionReceived;

			slidoClient.onSlidoConnected += (object args) =>
			{
				dispatcher?.BeginInvoke(new Action(() => { SlidoConnected = true; }));
			};
			slidoClient.onSlidoDisconnected += (object args) =>
			{
				dispatcher?.BeginInvoke(new Action(() => { SlidoConnected = false; }));
			};

			ButtonText = "Disconnect";

			await slidoClient.Connect();


			//終了時謎の websocket Exceptionが発生している
		}

		private async void SlidoClient_onSlidoNewQuestionReceived(object sender, SlidoWebSocketLib.Events.SlidoNewQuestionReceiveEventArgs args)
		{
			var sectionId = args.QuestonMessage.event_section_id;
			var section = Comments.Where(a => a.SessionID == sectionId).First();

			await dispatcher.BeginInvoke(new Action(() =>
			{
				section.Comments.Add(new CommentTipViewModel() { Author = args.QuestonMessage.author.name, Comment = args.QuestonMessage.text_formatted });

			}));

			if (!VoicevoxEnabled) return;

			//voicevoxEnabled = trueの時に実行
			var query = await voiceClient.GetQueries(args.QuestonMessage.text_formatted);
			await voiceClient.Synthesis(query);
			SoundPlayer player = new();
			player.SoundLocation = Environment.CurrentDirectory + "/test.wav";
			player.Load();
			player.Play();
		}

	}
}
