
namespace JOX.Assistant.Interface
{
    public interface IAzureAIService
    {
        public Task<Tuple<string, string>> AnalyzeSentimentAsync(string content);
    }
}
