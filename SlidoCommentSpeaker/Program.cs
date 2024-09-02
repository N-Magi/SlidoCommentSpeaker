// See https://aka.ms/new-console-template for more information
using SlidoWebSocketLib;
using VoicevoxRestLib;
using System.Media;



//var uri = new Uri(uriStr);
Console.WriteLine("Enter Access Token");
var accessToken = Console.ReadLine();
Console.WriteLine($"Enter Target");
var target = Console.ReadLine();

Console.WriteLine("Enter SlidoURL(Https://...");
var uriStr = Console.ReadLine();
var client = new SlidoClient();
var tokens = await client.GetTargetAndToken(uriStr, CancellationToken.None);
client.SetTokens(tokens);

client.onSlidoNewQuestionReceived += Client_onSlidoNewCommentRecived;

async void Client_onSlidoNewCommentRecived(object sender, SlidoWebSocketLib.Events.SlidoNewQuestionReceiveEventArgs args)
{
	VoicevoxClient vclient = new VoicevoxClient();
	var word = args.QuestonMessage.text_formatted;
	if (word == null) return;
	var q = await vclient.GetQueries(word);
	Console.WriteLine(q);
	var voiceStream = await vclient.Synthesis(q);

	SoundPlayer player = new();
	player.Stream = voiceStream;
	player.Load();
	player.Play();
}

await client.Connect();



