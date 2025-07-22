using OpenAI.Chat;


namespace JOX.Assistant.Interface
{
    public interface IOpenIAServices
    {
        void SetConfiguration(string urlOpenIa, string apiKey, string modelDep);

        Task<string> CreateChatCompletionAsync(IList<ChatMessage> requests, string modelFormat = "text", float temperature = (float)0.1);
    }
}
