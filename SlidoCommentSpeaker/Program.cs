// See https://aka.ms/new-console-template for more information
using SlidoWebSocketLib;
using VoicevoxRestLib;
using System.Media;



//var uri = new Uri(uriStr);
//Console.WriteLine("Enter Access Token");
//var accessToken = Console.ReadLine();
//Console.WriteLine($"Enter Target");
//var target = Console.ReadLine();

//jPewKtZUHNhNfANGEHR4aT
Console.WriteLine("Enter SlidoURL(Https://...");
var uriStr = Console.ReadLine();

var tokens = await SlidoClient.GetTargetAndToken(uriStr, CancellationToken.None);

var client = new SlidoClient(tokens);
client.onSlidoNewQuestionReceived += Client_onSlidoNewCommentRecived;

async void Client_onSlidoNewCommentRecived(object sender, SlidoWebSocketLib.Events.SlidoNewQuestionReceiveEventArgs args)
{
	VoicevoxClient vclient = new VoicevoxClient();
	var word = args.QuestonMessage.text_formatted;
	if (word == null) return;
	var q = await vclient.GetQueries(word);
	Console.WriteLine(q);
	await vclient.Synthesis(q);

	SoundPlayer player = new();
	player.SoundLocation = Environment.CurrentDirectory + "/test.wav";
	player.Load();
	player.Play();
}

await client.Connect();






