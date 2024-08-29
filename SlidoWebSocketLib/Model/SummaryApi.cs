using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlidoWebSocketLib.Model.SummaryApi
{
	public class SummaryApi
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
		public Section[] sections { get; set; }
		public string code { get; set; }
		public Captcha1 captcha { get; set; }
		public Flags flags { get; set; }
		public Flaginfo flagInfo { get; set; }
		public string dataMode { get; set; }
	}
	public class Section
	{
		public Attrs1 attrs { get; set; }
		public string uuid { get; set; }
		public int event_section_id { get; set; }
		public int event_id { get; set; }
		public int order { get; set; }
		public string name { get; set; }
		public bool is_active { get; set; }
		public bool is_deleted { get; set; }
		public Slidesdrive slidesdrive { get; set; }
	}

	public class Attrs1
	{
		public string color { get; set; }
	}

	public class Slidesdrive
	{
		public bool is_wall_displayed { get; set; }
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
		public bool show_highlighted_as_single { get; set; }
		public bool show_twitter { get; set; }
		public bool show_partners { get; set; }
		public bool show_logo { get; set; }
		public int template { get; set; }
		public int theme { get; set; }
		public bool transparent_logo_box { get; set; }
		public bool transparent_partners_box { get; set; }
		public int layout { get; set; }
		public int new_wall { get; set; }
		public int new_simple_wall { get; set; }
		public Questions questions { get; set; }
		public Polls polls { get; set; }
		public bool enable_word_cloud { get; set; }
		public bool enable_qr_code { get; set; }
		public bool enable_new_wall { get; set; }
		public bool enable_custom_theme { get; set; }
	}

	public class Questions
	{
		public int limit { get; set; }
		public int limit_latest { get; set; }
	}

	public class Polls
	{
		public int limit { get; set; }
		public bool fixed_order { get; set; }
		public bool hide_counter { get; set; }
	}

	public class Attrs
	{
		public Questions1 questions { get; set; }
		public Ideas ideas { get; set; }
		public Polls1 polls { get; set; }
		public bool moderate_twitter { get; set; }
		public bool moderate_questions { get; set; }
		public bool new_admin_used { get; set; }
		public bool enable_google_export { get; set; }
		public Announcement announcement { get; set; }
		public Crm crm { get; set; }
		public bool moderate_comments { get; set; }
		public bool moderate_ideas { get; set; }
		public bool is_experimental { get; set; }
		public bool force_select_section { get; set; }
		public bool enable_ideas { get; set; }
		public bool whitelabel { get; set; }
		public Signin signin { get; set; }
		public Captcha captcha { get; set; }
		public Agenda agenda { get; set; }
		public Social social { get; set; }
		public bool enable_welcome_screen { get; set; }
		public bool sso_requires_consent { get; set; }
		public General_Consent_Notice general_consent_notice { get; set; }
		public bool is_ama { get; set; }
		public bool is_gslides { get; set; }
		public bool is_powerpoint { get; set; }
		public bool hasActiveTrial { get; set; }
		public Branding_Colors branding_colors { get; set; }
		public bool enable_labels { get; set; }
		public bool enable_menu_links { get; set; }
		public bool enable_custom_app_theme { get; set; }
		public bool usedInLargeEvent { get; set; }
		public Menu menu { get; set; }
	}

	public class Questions1
	{
		public int max_length { get; set; }
		public bool enable_downvote { get; set; }
		public bool consume_tweets { get; set; }
		public bool consume_tweets_enabled { get; set; }
		public bool disable_anonymous { get; set; }
		public bool default_anonymous { get; set; }
		public bool anonymous_behavior_enforced { get; set; }
		public bool display_recent_first { get; set; }
		public string default_order { get; set; }
		public bool disable_new { get; set; }
		public bool disable_voting { get; set; }
	}

	public class Ideas
	{
		public int max_length { get; set; }
	}

	public class Polls1
	{
		public string results_display_type { get; set; }
		public bool hide_results { get; set; }
	}

	public class Announcement
	{
		public bool enabled { get; set; }
	}

	public class Crm
	{
		public int attendees_count { get; set; }
		public bool reporting_include { get; set; }
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

	public class Agenda
	{
		public bool show_presentations { get; set; }
		public bool enable_files_download { get; set; }
	}

	public class Social
	{
		public Facebook_Live facebook_live { get; set; }
	}

	public class Facebook_Live
	{
		public bool streaming { get; set; }
	}

	public class General_Consent_Notice
	{
		public bool enabled { get; set; }
		public string trigger { get; set; }
	}

	public class Branding_Colors
	{
	}

	public class Menu
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

	public class Flaginfo
	{
		public _201906EXPPS7962RecentTabAsDefault1 _201906EXPPS7962recenttabasdefault { get; set; }
		public _201911EXPPS9453QuestionEditRequest1 _201911EXPPS9453questioneditrequest { get; set; }
		public _201912EXPPS9641QuestionWithdraw1 _201912EXPPS9641questionwithdraw { get; set; }
		public _201912EXPPS9664ExternalLinks1 _201912EXPPS9664externallinks { get; set; }
		public _202006SeparateScoreForQuestionDownvotes1 _202006separatescoreforquestiondownvotes { get; set; }
		public _202008SimilarQuestions1 _202008similarquestions { get; set; }
		public _202103RandomQuizOptions1 _202103randomquizoptions { get; set; }
		public _202108QuestionsTopics1 _202108questionstopics { get; set; }
		public _202207OpenTextTopics1 _202207opentexttopics { get; set; }
		public _202207VirpoCrazyExperiments1 _202207virpocrazyexperiments { get; set; }
		public _202209TypingInAllPolls1 _202209typinginallpolls { get; set; }
		public _202210TypingInQa1 _202210typinginqa { get; set; }
		public _202302ParticipantSignupButtons1 _202302participantsignupbuttons { get; set; }
		public _202312TypingIndicator1 _202312typingindicator { get; set; }
		public _202304QuestionCopilot1 _202304questioncopilot { get; set; }
		public _202304QuestionCopilotSilly1 _202304questioncopilotsilly { get; set; }
		public _202310Chat1 _202310chat { get; set; }
		public _202310ChatReactions1 _202310chatreactions { get; set; }
		public _202403Translations1 _202403translations { get; set; }
		public _202406AiPolls1 _202406aipolls { get; set; }
		public _202407PasscodeInUrl1 _202407passcodeinurl { get; set; }
		public _202407EventAnalyticsQuestionSentiment1 _202407eventanalyticsquestionsentiment { get; set; }
		public _202408AiQuiz1 _202408aiquiz { get; set; }
		public _201908EXPPS7902SlidoLabs1 _201908EXPPS7902slidolabs { get; set; }
		public RedirectToApp21 redirecttoapp2 { get; set; }
		public RedirectToAdmin21 redirecttoadmin2 { get; set; }
		public TestingEventFoo1 testingeventfoo { get; set; }
	}

	public class _201906EXPPS7962RecentTabAsDefault1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value value { get; set; }
	}

	public class Value
	{
		public bool value { get; set; }
	}

	public class _201911EXPPS9453QuestionEditRequest1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value1 value { get; set; }
	}

	public class Value1
	{
		public bool value { get; set; }
	}

	public class _201912EXPPS9641QuestionWithdraw1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value2 value { get; set; }
	}

	public class Value2
	{
		public bool value { get; set; }
	}

	public class _201912EXPPS9664ExternalLinks1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value3 value { get; set; }
	}

	public class Value3
	{
		public bool value { get; set; }
	}

	public class _202006SeparateScoreForQuestionDownvotes1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value4 value { get; set; }
	}

	public class Value4
	{
		public bool value { get; set; }
	}

	public class _202008SimilarQuestions1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value5 value { get; set; }
	}

	public class Value5
	{
		public bool value { get; set; }
	}

	public class _202103RandomQuizOptions1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value6 value { get; set; }
	}

	public class Value6
	{
		public bool value { get; set; }
	}

	public class _202108QuestionsTopics1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value7 value { get; set; }
	}

	public class Value7
	{
		public bool value { get; set; }
	}

	public class _202207OpenTextTopics1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value8 value { get; set; }
	}

	public class Value8
	{
		public bool value { get; set; }
	}

	public class _202207VirpoCrazyExperiments1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value9 value { get; set; }
	}

	public class Value9
	{
		public bool value { get; set; }
	}

	public class _202209TypingInAllPolls1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value10 value { get; set; }
	}

	public class Value10
	{
		public bool value { get; set; }
	}

	public class _202210TypingInQa1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value11 value { get; set; }
	}

	public class Value11
	{
		public bool value { get; set; }
	}

	public class _202302ParticipantSignupButtons1
	{
		public string label { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value12 value { get; set; }
	}

	public class Value12
	{
		public bool value { get; set; }
	}

	public class _202312TypingIndicator1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value13 value { get; set; }
	}

	public class Value13
	{
		public bool value { get; set; }
	}

	public class _202304QuestionCopilot1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value14 value { get; set; }
	}

	public class Value14
	{
		public bool value { get; set; }
	}

	public class _202304QuestionCopilotSilly1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value15 value { get; set; }
	}

	public class Value15
	{
		public bool value { get; set; }
	}

	public class _202310Chat1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value16 value { get; set; }
	}

	public class Value16
	{
		public bool value { get; set; }
	}

	public class _202310ChatReactions1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value17 value { get; set; }
	}

	public class Value17
	{
		public bool value { get; set; }
	}

	public class _202403Translations1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value18 value { get; set; }
	}

	public class Value18
	{
		public bool value { get; set; }
	}

	public class _202406AiPolls1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value19 value { get; set; }
	}

	public class Value19
	{
		public bool value { get; set; }
	}

	public class _202407PasscodeInUrl1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value20 value { get; set; }
	}

	public class Value20
	{
		public bool value { get; set; }
	}

	public class _202407EventAnalyticsQuestionSentiment1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value21 value { get; set; }
	}

	public class Value21
	{
		public bool value { get; set; }
	}

	public class _202408AiQuiz1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value22 value { get; set; }
	}

	public class Value22
	{
		public bool value { get; set; }
	}

	public class _201908EXPPS7902SlidoLabs1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value23 value { get; set; }
	}

	public class Value23
	{
		public bool value { get; set; }
	}

	public class RedirectToApp21
	{
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value24 value { get; set; }
	}

	public class Value24
	{
		public bool value { get; set; }
	}

	public class RedirectToAdmin21
	{
		public object[] tags { get; set; }
		public string valueType { get; set; }
		public Value25 value { get; set; }
	}

	public class Value25
	{
		public bool value { get; set; }
	}

	public class TestingEventFoo1
	{
		public string label { get; set; }
		public string labelShort { get; set; }
		public string labelLong { get; set; }
		public string[] tags { get; set; }
		public string valueType { get; set; }
		public Value26 value { get; set; }
	}

	public class Value26
	{
		public bool value { get; set; }
	}

	


}
