﻿
using SlidoCommentSpeakerPluginBase;
using System;
using System.Text.Json.Nodes;
using VoicevoxRestLib;
using System.Media;


namespace VoicevoxPlugin
{
	public class VoicevoxPlugin : SlidoCommentSpeakerPluginBase.IPlugin
	{
		SoundPlayer player = new();
		private VoicevoxClient? voiceClient;
		public Queue<Stream>? voicevoxQueue { get; set; }
		private Task? voicevoxWorker;


		public string Name => "VoiceVox読み上げプラグイン";

		public string Version => "0.0.1";

		public string Description => "Voicevoxで読み上げてくれるプラグインです。";

		public event EventHandler? CanExecuteChanged;

		private IPluginContext _pluginCtx;
		public bool CanExecute(object? parameter)
		{
			return true;
		}

		public void Execute(object? parameter)
		{

		}

		public async void RunTask(CancellationToken token)
		{


			voicevoxWorker = Task.Run(async () =>
			{
				try
				{
					//voicevoxQueue.Enqueue("ヴォイスボックスに接続しました");
					voicevoxQueue.Enqueue(await SpeakVoicevox("ヴォイスボックスに接続しました"));
					while (!token.IsCancellationRequested)
					{
						if (voicevoxQueue.Count <= 0) { continue; }

						player.Stream = voicevoxQueue.Dequeue();
						player.Load();
						//コメント読み上げ中に新コメントの読み上げ防止
						player.PlaySync();
					}

				}
				catch (Exception ex)
				{

				}
			});

			voicevoxQueue.Clear();

			await voicevoxWorker;
		}

		public async void Init(IPluginContext context)
		{
			_pluginCtx = context;
			voicevoxQueue = new Queue<Stream>();
			voiceClient = new();
			_pluginCtx.OnCommentRecieved += Context_OnCommentRecieved;
		}

		public void Dispose()
		{
			_pluginCtx.OnCommentRecieved -= Context_OnCommentRecieved;
		}

		private async void Context_OnCommentRecieved(object sender, CommentRecievedEventArgs args)
		{
			Console.WriteLine(args.Comment);
			voicevoxQueue.Enqueue(await SpeakVoicevox(args.Comment));
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
