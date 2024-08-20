using System.Net.Http.Headers;

namespace VoicevoxRestLib
{
	public class VoicevoxClient
	{
		public async Task<string> GetQueries(string words)
		{
			HttpClient client = new HttpClient();
			var query = new Uri(@$"http://localhost:50021/audio_query?text={words}&speaker=3");

			var result = await client.PostAsync(query, null);
			return await result.Content.ReadAsStringAsync();
		}

		public async Task Synthesis(string query)
		{
			HttpClient client = new HttpClient();
			StringContent content = new StringContent(query);
			content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			//content.Headers.Add("accept", "audio/wav");

			var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost:50021/synthesis?speaker=3&enable_interrogative_upspeak=true");
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
