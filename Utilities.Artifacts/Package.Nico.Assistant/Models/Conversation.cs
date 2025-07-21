

namespace Nico.Assistant.Models
{
    /// <summary>
    /// Represents a conversation record with associated metadata, tags, and chat history.
    /// </summary>
    public class Conversation
    {        
        public Conversation()
        {
            DateStartInteraction = GetDate();
        }

        public static DateTime GetDate()
        {
            var zones = TimeZoneInfo.GetSystemTimeZones().Where(x => x.BaseUtcOffset.TotalHours.Equals(double.Parse("-05"))).FirstOrDefault();
            var fechaActual = DateTime.Now;
            return TimeZoneInfo.ConvertTime(fechaActual, zones);
        }

        public DateTime DateStartInteraction { get; set; }

        /// <summary>
        /// Unique identifier for the conversation.
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// Identifier for the conversation thread.
        /// </summary>
        public string SessionId { get; set; }

        /// <summary>
        /// Identifier for the person involved in the conversation.
        /// </summary>
        public string PersonId { get; set; }

        /// <summary>
        /// The date and time when the conversation was created.
        /// </summary>
        public DateTime DateCreate { get; set; }

        /// <summary>
        /// The date and time when the conversation was last modified.
        /// </summary>
        public DateTime DateModify { get; set; }

        /// <summary>
        /// Collection of tags associated with the conversation.
        /// </summary>
        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// Collection of chat messages within the conversation.
        /// </summary>
        public List<Chat> Chats { get; set; } = new List<Chat>();

        public IdentMessage From { get; set; } = new();

        public string RagText { get; set; }
        public string TypeOperation { get; set; }

        public string UserText { get; set; }

        public string CurrentText { get; set; }    

        public string ScoredIntent { get; set; } = "None";


        public string TypeChannel { get; set; } = "None";

        public string Calification { get; set; } = "None";
    }

    /// <summary>
    /// Represents a chat message within a conversation.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// The content of the chat message.
        /// </summary>
        public string UserText { get; set; } = string.Empty;

        /// <summary>
        /// text bot rag
        /// </summary>
        public string BotText { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the message is from the user or the system.
        /// </summary>
        public string Mode { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public string Calification { get; set; } = string.Empty;

        public Dictionary<string, string> Tags { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The date and time when the message was sent, in UTC.
        /// </summary>
        public DateTime Date { get; set; } = Conversation.GetDate();
    }
}
