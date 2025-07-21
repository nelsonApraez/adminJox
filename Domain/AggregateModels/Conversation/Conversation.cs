using System;
using System.Collections.Generic;
using Domain.AggregateModels.ValueObjects;
using Domain.Common;
using MongoDB.Bson.Serialization.Attributes;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Conversation)
    /// </summary>
    [BsonIgnoreExtraElements]
    public partial class Conversation : Domain.Common.Interfaces.IEntities
    { 
       public Conversation()
       { }

        public  virtual string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime DateStartInteraction { get; set; }


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

        public string ScoredIntent { get; set; } = "None";


        public string TypeChannel { get; set; } = "None";

        public string Calification { get; set; } = "None";

    }

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
        public DateTime Date { get; set; } 
    }

    public class IdentMessage
    {
        public string Id { get; set; }

        public string Dialogo { get; set; }

        public string Who { get; set; }

        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
