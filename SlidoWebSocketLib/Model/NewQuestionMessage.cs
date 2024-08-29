using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model
{
	public class NewQuestionMessage
	{
		public Author? author { get; set; }

		[JsonPropertyName("attrs")]
		public NewQuestionAttrs? attrs { get; set; }
		public string type { get; set; }
		public string text_formatted { get; set; }
		public int event_question_id { get; set; }
		public int event_id { get; set; }
		public int event_section_id { get; set; }
		public string text { get; set; }
		public bool is_public { get; set; }
		public bool is_answered { get; set; }
		public bool is_highlighted { get; set; }
		public bool is_anonymous { get; set; }
		public bool is_bookmarked { get; set; }
		public int score { get; set; }
		public int score_positive { get; set; }
		public int score_negative { get; set; }
		public object date_published { get; set; }
		public object date_highlighted { get; set; }
		public string path { get; set; }
		public DateTime date_created { get; set; }
		public DateTime date_updated { get; set; }
		public object date_deleted { get; set; }
		public object[] labels { get; set; }
		public object[] pinned_replies { get; set; }
	}

	public class Author
	{
		public object attrs { get; set; }
		public int event_user_id { get; set; }
		public string name { get; set; }
	}


	public class NewQuestionAttrs
	{
		public string language { get; set; }
		public string author_name { get; set; }
		public string sentiment { get; set; }
		public bool is_profane { get; set; }
		public object[] profanity_metadata { get; set; }
		public bool is_comment { get; set; }
	}

}
