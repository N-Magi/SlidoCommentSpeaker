using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model
{
	public class EventApi
	{
		public Url url { get; set; }
		public int out_of_date { get; set; }
		public Wall wall { get; set; }
		public Attrs attrs { get; set; }
		public Localized localized { get; set; }
		public object[] partners_files { get; set; }
		public int event_id { get; set; }
		public bool is_active { get; set; }
		public bool is_complete { get; set; }
		public bool is_deleted { get; set; }
		public int event_group_id { get; set; }
		public string locale { get; set; }
		public string timezone { get; set; }
		public string hash { get; set; }
		public object domain { get; set; }
		public string name { get; set; }
		public DateTime date_from { get; set; }
		public DateTime date_to { get; set; }
		public string location { get; set; }
		public object location_lat { get; set; }
		public object location_lng { get; set; }
		public object img { get; set; }
		public object description { get; set; }
		public object twitter { get; set; }
		public object twitter_hash { get; set; }
		public bool is_public { get; set; }
		public int plan_id { get; set; }
		public bool enable_agenda { get; set; }
		public bool enable_questions { get; set; }
		public bool enable_polls { get; set; }
		public bool enable_twitter { get; set; }
		public bool enable_comments { get; set; }
		public bool enable_networking { get; set; }
		public bool enable_registration { get; set; }
		public bool enable_captcha { get; set; }
		public bool enable_custom_messages { get; set; }
		public bool enable_presentations { get; set; }
		public bool enable_sections { get; set; }
		public DateTime date_created { get; set; }
		public DateTime date_updated { get; set; }
		public bool enable_autocomplete { get; set; }
		public string uuid { get; set; }
		public Pricingplan pricingPlan { get; set; }
		public object[] labels { get; set; }
		public string code { get; set; }
		public Captcha1 captcha { get; set; }
		public Flags flags { get; set; }
		public string dataMode { get; set; }
	}

	public class Url
	{
		public string app { get; set; }
		public string admin { get; set; }
		public string wall { get; set; }
		public string wall_direct { get; set; }
	}

	public class Wall
	{
		public bool transparent_logo_box { get; set; }
		public bool transparent_partners_box { get; set; }
	}

	public class Attrs
	{
		public Signin signin { get; set; }
		public Captcha captcha { get; set; }
		public bool whitelabel { get; set; }
		public bool enable_welcome_screen { get; set; }
		public Questions questions { get; set; }
		public bool sso_requires_consent { get; set; }
		public bool enable_ideas { get; set; }
		public General_Consent_Notice general_consent_notice { get; set; }
		public Crm crm { get; set; }
	}

	public class Signin
	{
	}

	public class Captcha
	{
		public bool enabled { get; set; }
		public Ratelimit rateLimit { get; set; }
	}

	public class Ratelimit
	{
		public int resetTimeframe { get; set; }
		public int requestsLimit { get; set; }
	}

	public class Questions
	{
		public bool disable_anonymous { get; set; }
		public bool default_anonymous { get; set; }
	}

	public class General_Consent_Notice
	{
		public bool enabled { get; set; }
		public string trigger { get; set; }
	}

	public class Crm
	{
	}

	public class Localized
	{
		public DateTime date_from { get; set; }
		public DateTime date_to { get; set; }
	}

	public class Pricingplan
	{
		public int id { get; set; }
		public string name { get; set; }
		public int weight { get; set; }
	}

	public class Captcha1
	{
		public bool required { get; set; }
	}

	public class Flags
	{
		public _201906EXPPS7962RecentTabAsDefault _201906EXPPS7962recenttabasdefault { get; set; }
		public _201911EXPPS9453QuestionEditRequest _201911EXPPS9453questioneditrequest { get; set; }
		public _201912EXPPS9641QuestionWithdraw _201912EXPPS9641questionwithdraw { get; set; }
		public _201912EXPPS9664ExternalLinks _201912EXPPS9664externallinks { get; set; }
		public _202006SeparateScoreForQuestionDownvotes _202006separatescoreforquestiondownvotes { get; set; }
		public _202008SimilarQuestions _202008similarquestions { get; set; }
		public _202103RandomQuizOptions _202103randomquizoptions { get; set; }
		public _202108QuestionsTopics _202108questionstopics { get; set; }
		public _202207OpenTextTopics _202207opentexttopics { get; set; }
		public _202207VirpoCrazyExperiments _202207virpocrazyexperiments { get; set; }
		public _202209TypingInAllPolls _202209typinginallpolls { get; set; }
		public _202210TypingInQa _202210typinginqa { get; set; }
		public _202302ParticipantSignupButtons _202302participantsignupbuttons { get; set; }
		public _202312TypingIndicator _202312typingindicator { get; set; }
		public _202304QuestionCopilot _202304questioncopilot { get; set; }
		public _202304QuestionCopilotSilly _202304questioncopilotsilly { get; set; }
		public _202310Chat _202310chat { get; set; }
		public _202310ChatReactions _202310chatreactions { get; set; }
		public _202403Translations _202403translations { get; set; }
		public _202406AiPolls _202406aipolls { get; set; }
		public _202407PasscodeInUrl _202407passcodeinurl { get; set; }
		public _202407EventAnalyticsQuestionSentiment _202407eventanalyticsquestionsentiment { get; set; }
		public _202408AiQuiz _202408aiquiz { get; set; }
		public _201908EXPPS7902SlidoLabs _201908EXPPS7902slidolabs { get; set; }
		public RedirectToApp2 redirecttoapp2 { get; set; }
		public RedirectToAdmin2 redirecttoadmin2 { get; set; }
		public TestingEventFoo testingeventfoo { get; set; }
	}

	public class _201906EXPPS7962RecentTabAsDefault
	{
		public bool value { get; set; }
	}

	public class _201911EXPPS9453QuestionEditRequest
	{
		public bool value { get; set; }
	}

	public class _201912EXPPS9641QuestionWithdraw
	{
		public bool value { get; set; }
	}

	public class _201912EXPPS9664ExternalLinks
	{
		public bool value { get; set; }
	}

	public class _202006SeparateScoreForQuestionDownvotes
	{
		public bool value { get; set; }
	}

	public class _202008SimilarQuestions
	{
		public bool value { get; set; }
	}

	public class _202103RandomQuizOptions
	{
		public bool value { get; set; }
	}

	public class _202108QuestionsTopics
	{
		public bool value { get; set; }
	}

	public class _202207OpenTextTopics
	{
		public bool value { get; set; }
	}

	public class _202207VirpoCrazyExperiments
	{
		public bool value { get; set; }
	}

	public class _202209TypingInAllPolls
	{
		public bool value { get; set; }
	}

	public class _202210TypingInQa
	{
		public bool value { get; set; }
	}

	public class _202302ParticipantSignupButtons
	{
		public bool value { get; set; }
	}

	public class _202312TypingIndicator
	{
		public bool value { get; set; }
	}

	public class _202304QuestionCopilot
	{
		public bool value { get; set; }
	}

	public class _202304QuestionCopilotSilly
	{
		public bool value { get; set; }
	}

	public class _202310Chat
	{
		public bool value { get; set; }
	}

	public class _202310ChatReactions
	{
		public bool value { get; set; }
	}

	public class _202403Translations
	{
		public bool value { get; set; }
	}

	public class _202406AiPolls
	{
		public bool value { get; set; }
	}

	public class _202407PasscodeInUrl
	{
		public bool value { get; set; }
	}

	public class _202407EventAnalyticsQuestionSentiment
	{
		public bool value { get; set; }
	}

	public class _202408AiQuiz
	{
		public bool value { get; set; }
	}

	public class _201908EXPPS7902SlidoLabs
	{
		public bool value { get; set; }
	}

	public class RedirectToApp2
	{
		public bool value { get; set; }
	}

	public class RedirectToAdmin2
	{
		public bool value { get; set; }
	}

	public class TestingEventFoo
	{
		public bool value { get; set; }
	}

}
