using System.Net.Http.Headers;

namespace VoicevoxRestLib
{
	public class VoicevoxClient
	{
		HttpClient client;

		public VoicevoxClient()
		{
			HttpClientHandler handler = new HttpClientHandler();
			handler.UseProxy = false;
			handler.UseCookies = false;
			handler.PreAuthenticate = false;
			handler.AllowAutoRedirect = false;


			client = new HttpClient(handler);
		}
		public async Task<string> GetQueries(string words)
		{
			var query = new HttpRequestMessage();
			query.RequestUri = new Uri(@$"http://127.0.0.1:50021/audio_query?text={words}&speaker=3");

			query.Headers.Add("Accept", "application/json");
			query.Method = HttpMethod.Post;

			
			var result = client.Send(query, CancellationToken.None);
			var res = await result.Content.ReadAsStringAsync();
			return res;
		}

		public async Task<Stream> Synthesis(string query)
		{
			//HttpClient client = new HttpClient();
			StringContent content = new StringContent(query);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			//content.Headers.Add("accept", "audio/wav");

			var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:50021/synthesis?speaker=3&enable_interrogative_upspeak=true");
			request.Content = content;

			var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);


			return await response.Content.ReadAsStreamAsync();
		}

		public async Task Synthesis(string query,string filepath)
		{
			//HttpClient client = new HttpClient();
			StringContent content = new StringContent(query);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			//content.Headers.Add("accept", "audio/wav");

			var request = new HttpRequestMessage(HttpMethod.Post, "http://127.0.0.1:50021/synthesis?speaker=3&enable_interrogative_upspeak=true");
			request.Content = content;

			var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
			if (response.StatusCode == System.Net.HttpStatusCode.OK)
			{
				using (var fs = new FileStream("./test.wav", FileMode.OpenOrCreate))
				{
					var stream = await response.Content.ReadAsStreamAsync();
					stream.CopyTo(fs);
				}
			}
			else { Console.WriteLine("failed"); }

			return;

		}





	}
}
