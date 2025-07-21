using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Domain.Models
{
    public class IAModel
    {
        [JsonProperty("history")]
        public List<History>? History { get; set; }
        [JsonProperty("approach")]
        public string? Approach { get; set; }
        [JsonProperty("overrides")]
        public Overrides? Overrides { get; set; }
    }

    public class Overrides
    {
        [JsonProperty("semantic_ranker")]
        public string? SemanticRanker { get; set; }
        [JsonProperty("semantic_captions")]
        public string? SemanticCaptions { get; set; }
        [JsonProperty("top")]
        public int Top { get; set; }
        [JsonProperty("suggest_followup_questions")]
        public string? SuggestFollowupQuestions { get; set; }
        [JsonProperty("user_persona")]
        public string? UserPersona { get; set; }
        [JsonProperty("system_persona")]
        public string? SystemPersona { get; set; }
        [JsonProperty("ai_persona")]
        public string? AiPersona { get; set; }
        [JsonProperty("response_length")]
        public int ResponseLength { get; set; }
        [JsonProperty("response_temp")]
        public double ResponseTemp { get; set; }
        [JsonProperty("selected_folders")]
        public string? SelectedFolders { get; set; }
        [JsonProperty("selected_tags")]
        public string? SelectedTags { get; set; }
    }

    public class History
    {
        [JsonProperty("user")]
        public string? User { get; set; }
        [JsonProperty("bot")]
        public string? Bot { get; set; }
    }

    public class IAResponse
    {
        [JsonProperty("answer")]
        public string? Answer { get; set; }
        [JsonProperty("thoughts")]
        public string? Thoughts { get; set; }
    }
}
