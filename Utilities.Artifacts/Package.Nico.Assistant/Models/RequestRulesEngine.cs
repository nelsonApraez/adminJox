using Newtonsoft.Json;
using System.Text.Json;


namespace Nico.Assistant.Models
{
    public class RequestRulesEngine
    {
        [JsonProperty("ConversationId")]
        public string ConversationId { get; set; }

        [JsonProperty("ResourceName")]
        public string ResourceName { get; set; }
        [JsonProperty("PersonId")]
        public string PersonId { get; set; }

        [JsonProperty("Action")]
        public string Action { get; set; }
        [JsonProperty("Intention")]
        public string Intention { get; set; }
        [JsonProperty("Params")]
        public Params Params { get; set; }
        [JsonProperty("Content")]
        public Content Content { get; set; }
    }
    public class Params
    {
        [JsonProperty("organization")]
        public string Organization { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
    public class Content
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
