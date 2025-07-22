using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JOX.Assistant.Models
{
    public class ChatMessageResource
    {
        public string MessageFromId { get; set; }
        public string MessageId { get; set; }= new Guid().ToString();
        public string AuthorName { get; set; }
        public string HtmlContent { get; set; }
        public DateTime Created { get; set; }= DateTime.Now;
    }
}
