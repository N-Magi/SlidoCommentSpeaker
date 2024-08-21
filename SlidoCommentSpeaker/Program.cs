// See https://aka.ms/new-console-template for more information
using SlidoWebSocketLib;
using VoicevoxRestLib;
using System.Media;

Console.WriteLine("Enter Uri(start with wss://...)");
var uriStr = Console.ReadLine();
var client = new SlidoClient();
var uri = new Uri(uriStr);
Console.WriteLine("Enter Access Token");
var accessToken = Console.ReadLine();
Console.WriteLine($"Enter Target");
var target = Console.ReadLine();
client.onSlidoNewQuestionRecived += Client_onSlidoNewCommentRecived;
client.onWsRecive += Client_onWsRecive;

void Client_onWsRecive(object sender, WsReciveEventArgs args)
{
	Console.WriteLine($"{args.slidoMessage.Code}/{args.slidoMessage.Message}");
	if (args.slidoMessage.Code == 40)
	{
		var client = (SlidoClient)sender;
		client.SubscribeSession(accessToken, target);
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






