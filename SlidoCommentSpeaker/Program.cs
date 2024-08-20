// See https://aka.ms/new-console-template for more information
using SlidoWebSocketLib;
using VoicevoxRestLib;
using System.Media;

Console.WriteLine("Hello, World!");
var client = new SlidoClient();
var uri = new Uri("wss://app.sli.do/eu1/stream/v0.5/stream-sio/?slidoappVersion=SlidoParticipantApp%2F57.112.3%20(web)&clientId=FL95vpQtwjIw0Tf&EIO=4&transport=websocket&t=P5lBslZ");

client.onSlidoNewQuestionRecived += Client_onSlidoNewCommentRecived;
client.onWsRecive += Client_onWsRecive;

void Client_onWsRecive(object sender, WsReciveEventArgs args)
{
	Console.WriteLine($"{args.slidoMessage.Code}/{args.slidoMessage.Message}");
	if (args.slidoMessage.Code == 40)
	{
		var client = (SlidoClient)sender;
		client.SubscribeSession("d5cf042500fe2b5b91f88c468fb510a557ffae61.eu1", "slido/events/41174063-5514-4874-8793-c9c154810a44/*");
	}
};

async void Client_onSlidoNewCommentRecived(object sender, SlidoWebSocketLib.Model.NewQuestionMessage? msg)
{
	VoicevoxClient vclient = new VoicevoxClient();
	var word = msg?.text_formatted;
	if (word == null) return;
	var q = await vclient.GetQueries(word);
	Console.WriteLine(q);
	await vclient.Synthesis(q);

	SoundPlayer player = new();
	player.SoundLocation = Environment.CurrentDirectory + "/test.wav";
	player.Load();
	player.Play();
}

await client.connect(uri);






