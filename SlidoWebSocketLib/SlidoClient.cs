using System.Net.WebSockets;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Text;
using SlidoWebSocketLib.Model;
using System.Text.Json;
using System.Net.Sockets;

namespace SlidoWebSocketLib
{
	public class WsReciveEventArgs : EventArgs
	{
		public SlidoMessage slidoMessage { get; set; }
		public ClientWebSocket? wsSocket { get; set; }
	}

	public class SlidoClient
	{

		ClientWebSocket wsClient = new ClientWebSocket();

		Queue<string> msgQueue = new Queue<string>();


		public delegate void onWsReciveEventHandler(object sender, WsReciveEventArgs args);
		public event onWsReciveEventHandler? onWsRecive;

		public delegate void onSlidoNewQuestionReciveEventHandler(object sender, NewQuestionMessage? qMsg);
		public event onSlidoNewQuestionReciveEventHandler? onSlidoNewQuestionRecived;

		public delegate void onSlidoSectionUpdateEventHandler(object sender, SectionsUpdateMessage? sMsg);
		public event onSlidoSectionUpdateEventHandler? onSlidoSectionUpdate;

		public delegate void onSlidoWallStatusMessageReceivedEventHandler(object sender, WallStatusMessage? wMsg);
		public event onSlidoWallStatusMessageReceivedEventHandler? onSlidoWallStatusMessageReceived;


		public string? AccessToken { get; set; }
		public string? Target { get; set; }

		public CancellationToken cancellationToken { get; set; } = CancellationToken.None;

		public async Task<bool> GetTargetAndToken(string url)
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

			Target = $"slido/events/{events.uuid}/*";
			AccessToken = auth.access_token;

			return true;
		}

		public async Task<bool> Connect()
		{
			var slidoWebSocketUri = new Uri("wss://app.sli.do/eu1/stream/v0.5/stream-sio/?slidoappVersion=SlidoParticipantApp%2F57.113.3%20(web)&EIO=4&transport=websocket");
			return await Connect(slidoWebSocketUri);
		}

		public async Task<bool> Connect(Uri uri)
		{
			await wsClient.ConnectAsync(uri, cancellationToken);

			//WebSocketのKeepAlive
			onWsRecive += (sender, args) =>
			{
				if (args.slidoMessage.Code == 2)
					msgQueue.Enqueue("3");
			};

			//初期接続時のSID取得
			onWsRecive += (sender, args) =>
			{
				if (args.slidoMessage.Code == 0)
				{
					msgQueue.Enqueue("40");

				}
			};

			onWsRecive += (sender, args) =>
			{
				if (args.slidoMessage.Code == 42)
				{
					var jsonMessage = args.slidoMessage.Message;
					var jsonNodes = System.Text.Json.Nodes.JsonNode.Parse(jsonMessage);
					var instruction = jsonNodes?.AsArray()[0];

					if (instruction?.GetValue<string>() == "newQuestion")
					{
						var paramss = getParams<NewQuestionMessage>(jsonNodes);
						onSlidoNewQuestionRecived?.Invoke(this, paramss);
						return;
					}

					if (instruction?.GetValue<string>() == "event.sections.update")
					{
						onSlidoSectionUpdate?.Invoke(this, getParams<SectionsUpdateMessage>(jsonNodes));
						return;
					}

					if (instruction?.GetValue<string>() == "wall.status")
					{
						onSlidoWallStatusMessageReceived?.Invoke(this, getParams<WallStatusMessage>(jsonNodes));
						return;
					}
				}
			};

			return await Loop();
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
				Console.WriteLine("parse failed");
				return default(SlidoMessage);
			}

			if (message == null)
				return new SlidoMessage { Code = code, Message = "" };

			var msg = msgRegex.Match(message).Value;

			return new SlidoMessage { Code = code, Message = msg };

		}


		public async Task<bool> Loop()
		{

			var awaiter = Task.Run(async () =>
			{
				while (true)
				{
					var rawMsg = await ReciveMessageAsync(wsClient);

					if (wsClient.State == WebSocketState.Closed)
					{
						return;
					}

					var slidoMsg = GetSlidoMesasge(rawMsg);
					onWsRecive?.Invoke(this, new WsReciveEventArgs { slidoMessage = slidoMsg, wsSocket = wsClient });
				}
			}).GetAwaiter();

			await Task.Run(async () =>
			{
				while (true)
				{
					if (msgQueue.Count <= 0) continue;

					var message = msgQueue.Dequeue();
					Console.WriteLine(message);
					await wsClient.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, cancellationToken);
				}
			});

			return true;
		}
	}
}
