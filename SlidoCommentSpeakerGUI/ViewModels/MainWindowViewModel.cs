using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using SlidoCommentSpeakerGUI.ViewModels;
using SlidoWebSocketLib;
using VoicevoxRestLib;
using SlidoCommentSpeakerPluginBase;
using SlidoCommentSpeakerGUI.Model;
using System.Windows.Input;

namespace SlidoCommentSpeakerGUI
{
	class MainWindowViewModel : ViewModelBase
	{

		private PluginContext pContext = new();

		SlidoWebSocketLib.SlidoClient slidoClient = null;

		private readonly Dispatcher dispatcher;

		SoundPlayer player = new();
		private VoicevoxClient voiceClient;
		public Queue<Stream> voicevoxQueue { get; set; }
		private Task voicevoxWorker;

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


		CancellationTokenSource cts = new();

		private bool _voicevoxEnabled = false;
		public bool VoicevoxEnabled
		{
			get => _voicevoxEnabled;
			set
			{
				_voicevoxEnabled = value;
				if (value)
				{

					try
					{
						if (value)
						{
							plugins[0].RunTask(cts.Token);
						}
						else
						{
							cts.Cancel();
						}
					}
					catch (Exception ex)
					{
						dispatcher?.BeginInvoke(() =>
						{
							MessageBox.Show(ex.Message, "Error");
							VoicevoxEnabled = false;
						}, null);
					}

				}
				NotifyPropertyChanged();
			}
		}
		public RelayCommand ConnectButtonPressed { get; set; }

		List<IPlugin> plugins = new List<IPlugin>();

		public MainWindowViewModel()
		{
			ConnectButtonPressed = new RelayCommand(_ => { onConnectButtonPressed(); });
			//PropertyChanged += MainWindowViewModel_PropertyChanged;
			dispatcher = Dispatcher.CurrentDispatcher;
			voicevoxQueue = new Queue<Stream>();
			voiceClient = new();

			var files = Directory.GetFiles("./plugins/", "*.dll");
			

			foreach (var file in files)
			{
				//ここ絶対に直せ　汚すぎる　不安定すぎ　ｳﾝｺｰﾄﾞ
				PluginLoader loader = new PluginLoader(file.Replace("./","\\"));
				var filename = file.Replace("./plugins/", "");
				var asm = loader.LoadFromAssemblyName(new System.Reflection.AssemblyName(filename.Remove(filename.Length - 4, 4)));
				int count = 0;
				foreach (Type type in asm.GetTypes())
				{
					if (typeof(IPlugin).IsAssignableFrom(type))
					{
						IPlugin result = Activator.CreateInstance(type) as IPlugin;
						result?.Init(pContext);
						plugins.Add(result);
						if (result != null)
						{
							break;
						}
					}
				}
			}
		}


		/// <summary>
		/// 接続・切断ボタン押下時の処理
		/// </summary>
		public async void onConnectButtonPressed()
		{
			if (SlidoUrl == string.Empty || SlidoUrl == null) return;

			//接続中の押下は切断ボタン　切断処理
			if (slidoClient?.isConnected != null)
			{

				Comments.Clear();
				ButtonText = "Connect";
				await slidoClient.DisconnectAsync();
				slidoClient.Dispose();
				return;
			}

			//待ち受けスレッドを新調
			slidoClient = new();
			var tokens = await slidoClient.GetTargetAndToken(SlidoUrl, CancellationToken.None);
			slidoClient.SetTokens(tokens);

			//sectionの数だけVMを作成
			foreach (var section in slidoClient.Summary.sections)
			{
				var commentListVM = new CommentListViewModel();
				commentListVM.SessionName = section.name;
				commentListVM.SessionID = section.event_section_id;
				Comments.Add(commentListVM);
			}

			slidoClient.onSlidoNewQuestionReceived += SlidoClient_onSlidoNewQuestionReceived;

			//ステータス表示に関するイベントの登録
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

			await dispatcher.BeginInvoke(new Action(async () =>
			{
				section.Comments.Add(new CommentTipViewModel() { Author = args.QuestonMessage.author.name, Comment = args.QuestonMessage.text_formatted });
			}));


			if (VoicevoxEnabled)
			{
				pContext.InvokeOnCommentRecived(args.QuestonMessage.text_formatted);
				voicevoxQueue.Enqueue(await SpeakVoicevox(args.QuestonMessage.text_formatted));
			}
		}

		private async Task<Stream> SpeakVoicevox(string word)
		{
			//voicevoxEnabled = trueの時に実行
			var query = await voiceClient.GetQueries(word);
			var queryNode = JsonNode.Parse(query);
			//残存コメ数に応じて読み上げ速度を向上させる
			queryNode["speedScale"] = Math.Pow(Math.E, (float)(voicevoxQueue.Count) / 5.0f) + 0.25;
			//音声合成
			var stream = await voiceClient.Synthesis(queryNode.ToJsonString());
			return stream;
		}
	}
}
