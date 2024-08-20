// See https://aka.ms/new-console-template for more information
using SlidoWebSocketLib;
using VoicevoxRestLib;
using System.Media;

Console.WriteLine("Hello, World!");
var client = new SlidoClient();
var uri = new Uri("wss://app.sli.do/eu1/stream/v0.5/stream-sio/?slidoappVersion=SlidoParticipantApp%2F57.101.2%20(web)&clientId=F7ZREtj7ikuPqyr&EIO=4&transport=websocket&t=P5FX4da");

client.onSlidoNewCommentRecived += Client_onSlidoNewCommentRecived;
client.onWsRecive += Sclient_onWsRecive;

async void Client_onSlidoNewCommentRecived(object sender, System.Text.Json.Nodes.JsonNode? node)
{
	VoicevoxClient vclient = new VoicevoxClient();
	var word = node["params"]["text_formatted"]?.GetValue<string>();
	if (word == null) return;
	var q = await vclient.GetQueries(word);
	Console.WriteLine(q);
	await vclient.Synthesis(q);

	SoundPlayer player = new();
	player.SoundLocation = Environment.CurrentDirectory + "/test.wav";
	player.Load();
	player.Play();
}

static void Sclient_onWsRecive(object sender, WsReciveEventArgs args)
{
	Console.WriteLine($"{args.slidoMessage.Code}/{args.slidoMessage.Message}");
	if (args.slidoMessage.Code == 40)
	{
		var client = (SlidoClient)sender;
		client.SubscribeSession("b8c297fc4a92877afaa9b55c48fe9a0a09cc1ec8.eu1", "slido/events/86b060cf-4f3a-47fb-8f91-b3e1d53eceef/*");
	}
}
await client.connect(uri);






