using Domain.AggregateModels;
using System.Collections.Generic;

namespace Application.Features.Models.Dto
{
    /// <summary>
    /// Esta clase representa la Implementacion DTO para la Entidad (Conversation)
    /// </summary>
    public partial class ConversationDto 
    { 
       public string Id { get; set; }

       public string Sessionid { get; set; }

       public string Personid { get; set; }

       public System.DateTime Datestartinteraction { get; set; }

       public System.DateTime Datecreate { get; set; }

       public System.DateTime Datemodify { get; set; }

        public Dictionary<string, string>? Tags { get; set; }

        public List<Chat> Chats { get; set; } = new List<Chat>();

        public IdentMessage From { get; set; } = new();

        public string Ragtext { get; set; }

       public string Typeoperation { get; set; }

       public string Usertext { get; set; }

       public string Scoredintent { get; set; }

       public string Typechannel { get; set; }

       public string Calification { get; set; }

 
    }

}
