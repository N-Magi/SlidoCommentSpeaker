using System.Net.WebSockets;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text;
using SlidoWebSocketLib.Model;
using System.Text.Json;
using System.Net.Sockets;
using SlidoWebSocketLib.Events;
using System.Reflection;
using System.Collections.Concurrent;
using static SlidoWebSocketLib.SlidoClient;
using System;
using System.ComponentModel;


namespace SlidoWebSocketLib
{
	public class SlidoClient : IDisposable
	{

		ClientWebSocket wsClient = new ClientWebSocket();

		Queue<string> msgQueue = new Queue<string>();


		public delegate void onWsReciveEventHandler(object sender, WsReceiveEventArgs args);
		public event onWsReciveEventHandler? onWsRecive;

		public delegate void onSlidoNewQuestionReceiveEventHandler(object sender, SlidoNewQuestionReceiveEventArgs args);
		public event onSlidoNewQuestionReceiveEventHandler? onSlidoNewQuestionReceived;

		public delegate void onSlidoSectionUpdateEventHandler(object sender, SectionUpdateEventArgs args);
		public event onSlidoSectionUpdateEventHandler? onSlidoSectionUpdate;

		public delegate void onSlidoWallStatusMessageReceivedEventHandler(object sender, WallStatusMessgeReceivedEventArgs args);
		public event onSlidoWallStatusMessageReceivedEventHandler? onSlidoWallStatusMessageReceived;

		public delegate void onSlidoDisconnectedEventHandler(object sender);
		public event onSlidoDisconnectedEventHandler? onSlidoDisconnected;

		public delegate void onSlidoConnectedEventHandler(object sender);
		public event onSlidoConnectedEventHandler? onSlidoConnected;

		public string? AccessToken { get; set; }
		public string? Target { get; set; }

		public bool isConnected
		{
			get
			{
				return wsClient.State == WebSocketState.Open || wsClient.State == WebSocketState.Connecting;
			}
		}


		public CancellationToken cancellationToken { get; set; } = CancellationToken.None;

		public static async Task<(string target, string accessToken)> GetTargetAndToken(string url, CancellationToken cancellationToken)
		{
			Uri uri = new Uri(url);
			var hash = uri.Segments[2].Replace("/", "");

			//イベントuuidの取得
			var eventApiUrl = "https://app.sli.do/eu1/api/v0.5/app/events?hash=" + hash;
			HttpClient client = new HttpClient();
			client.BaseAddress = uri;
			var eventResponse = await client.GetAsync(eventApiUrl, cancellationToken);
			var res = await eventResponse.Content.ReadAsStringAsync();
			var eventJsonNode = JsonNode.Parse(res);
			var events = eventJsonNode.Deserialize<Model.EventApi>();
			if (events == null)
				throw new Exception("falied to parse events api response");

			//アクセストークンの取得
			var authApiUrl = @$"https://app.sli.do/eu1/api/v0.5/events/{events.uuid}/auth";
			var content = new StringContent("{\"initialAppViewer\":\"browser--other\",\"granted_consents\":[\"StoreEssentialCookies\"]}", Encoding.UTF8);
			content.Headers.ContentType.MediaType = "application/json";
			var authResponse = await client.PostAsync(authApiUrl, content, cancellationToken);
			var authJsonNode = JsonNode.Parse(await authResponse.Content.ReadAsStringAsync());
			var auth = authJsonNode.Deserialize<Model.AuthApi>();
			if (auth == null)
				throw new Exception("failed to parse auth api response");


			return ($"slido/events/{events.uuid}/*", auth.access_token);
		}

		public SlidoClient((string target, string accessToken) tokens)
		{
			Target = tokens.target;
			AccessToken = tokens.accessToken;
		}

		public SlidoClient(string target, string accessToken)
		{
			Target = target;
			AccessToken = accessToken;
		}

		public SlidoClient()
		{

		}

		public void SetTokens((string target, string accessToken) tokens)
		{
			Target = tokens.target;
			AccessToken = tokens.accessToken;
		}

		public async Task Connect(bool autoSubscribe = true)
		{
			var slidoWebSocketUri = new Uri("wss://app.sli.do/eu1/stream/v0.5/stream-sio/?slidoappVersion=SlidoParticipantApp%2F57.113.3%20(web)&EIO=4&transport=websocket");


			await Connect(slidoWebSocketUri, autoSubscribe);
			return;
		}

		public async Task Connect(Uri uri, bool autoSubscribe = true)
		{
			
			await wsClient.ConnectAsync(uri, cancellationToken);

			//WebSocketのKeepAlive
			onWsRecive += (sender, args) =>
			{

				if (args.SlidoMessage.Code == 2)
					msgQueue.Enqueue("3");
			};

			//初期接続時のSID取得
			onWsRecive += (sender, args) =>
			{
				if (args.SlidoMessage.Code == 0)
				{
					msgQueue.Enqueue("40");

				}
			};

			//Slido セッション接続
			onWsRecive += (sender, args) =>
			{
				if (args.SlidoMessage.Code == 40)
				{
					SubscribeSession();
				}
			};

			//Slidoイベント発火
			onWsRecive += (sender, args) =>
			{
				if (args.SlidoMessage.Code == 430)
				{
					onSlidoConnected?.Invoke(this);
				}
			};



			onWsRecive += (sender, args) =>
			{
				if (args.SlidoMessage.Code == 42)
				{
					var jsonMessage = args.SlidoMessage.Message;
					var jsonNodes = System.Text.Json.Nodes.JsonNode.Parse(jsonMessage);
					var instruction = jsonNodes?.AsArray()[0];

					if (instruction?.GetValue<string>() == "newQuestion")
					{
						var questionMsg = getParams<NewQuestionMessage>(jsonNodes);
						onSlidoNewQuestionReceived?.Invoke(this, new SlidoNewQuestionReceiveEventArgs() { QuestonMessage = questionMsg });
						return;
					}

					if (instruction?.GetValue<string>() == "event.sections.update")
					{
						onSlidoSectionUpdate?.Invoke(this, new SectionUpdateEventArgs() { UpdateMessage = getParams<SectionsUpdateMessage>(jsonNodes) });
						return;
					}

					if (instruction?.GetValue<string>() == "wall.status")
					{
						onSlidoWallStatusMessageReceived?.Invoke(this, new WallStatusMessgeReceivedEventArgs() { WallStatusMessage = getParams<WallStatusMessage>(jsonNodes) });
						return;
					}
				}
			};

			await Loop();
			return;
		}

		private ParamType? getParams<ParamType>(JsonNode? node)
		{
			var body = node?.AsArray()?[1]?["params"]; //コメント処理系のちゃんとした実装が必要になるかも
			if (body == null)
				return default(ParamType);
			var newMsg = body.Deserialize<ParamType>(JsonSerializerOptions.Default);
			return newMsg;
		}

		public void SubscribeSession()
		{
			SubscribeSession(AccessToken, Target);
		}

		public void SubscribeSession(string accessToken, string targets)
		{
			string subscribeMessage = $"420[\"subscribe\",{{\"accessToken\":\"{accessToken}\",\"targets\":[\"{targets}\"]}}]";
			msgQueue.Enqueue(subscribeMessage);
		}
		public void SendMessage(SlidoMessage smsg)
		{
			string msg = smsg.Code + smsg.Message;
			msgQueue.Enqueue(msg);
		}


		private async Task<string?> ReciveMessageAsync(ClientWebSocket wsock)
		{
			var message = "";
			var eomFlag = false;
			do
			{
				var buffer = new byte[1024];
				if (wsock.State != WebSocketState.Open) return null;
				var result = await wsClient.ReceiveAsync(buffer, cancellationToken);
				if (result.MessageType == WebSocketMessageType.Close)
				{
					await wsClient.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close", cancellationToken);

					return null;
				}
				ArraySegment<byte> segment = new ArraySegment<byte>(buffer, 0, result.Count);
				var recMsg = Encoding.UTF8.GetString(segment);
				message += recMsg;
				eomFlag = result.EndOfMessage;
			} while (eomFlag == false);

			return message;
		}


		private SlidoMessage GetSlidoMesasge(string message)
		{

			Regex codeRegex = new Regex(@"^\d+", RegexOptions.None);
			Regex msgRegex = new Regex(@"\[.+\]$", RegexOptions.None);

			int code;
			var value = int.TryParse(codeRegex.Match(message).Value, out code);
			if (!value)
			{

				return default(SlidoMessage);
			}

			if (message == null)
				return new SlidoMessage { Code = code, Message = "" };

			var msg = msgRegex.Match(message).Value;

			return new SlidoMessage { Code = code, Message = msg };

		}

		public async Task DisconnectAsync()
		{
			
			await wsClient.CloseAsync(WebSocketCloseStatus.Empty, null, cancellationToken);
		}

		public async Task Loop()
		{
			var exeptions = new ConcurrentQueue<Exception>();
			var loopFlag = true; //true for loop, false for stop;
			var awaiter = Task.Run(async () =>
			{
				while (loopFlag && isConnected)
				{
					try
					{
						var rawMsg = await ReciveMessageAsync(wsClient);

						if (wsClient.State == WebSocketState.Closed)
						{
							loopFlag = false;
							break;
						}

						var slidoMsg = GetSlidoMesasge(rawMsg);
						onWsRecive?.Invoke(this, new WsReceiveEventArgs { SlidoMessage = slidoMsg, WsSocket = wsClient });
					}
					catch (Exception ex)
					{
						exeptions.Enqueue(ex);
						loopFlag = false;
					}
				}
			}).GetAwaiter();

			await Task.Run(async () =>
			{
				while (loopFlag && isConnected)
				{
					try
					{
						if (msgQueue.Count <= 0) continue;

						var message = msgQueue.Dequeue();

						await wsClient.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, cancellationToken);
					}
					catch (Exception ex)
					{
						exeptions.Enqueue(ex);
						loopFlag = false;
					}
				}
			});
			msgQueue.Clear();

			onSlidoDisconnected?.Invoke(this);

			if (!exeptions.IsEmpty)
			{
				throw new AggregateException(exeptions);
			}
			var _ = await ReciveMessageAsync(wsClient);
			return;
		}

		public void Dispose()
		{
			wsClient?.Dispose();
			msgQueue = null;
			AccessToken = null;
			Target = null;
		}
	}
}
