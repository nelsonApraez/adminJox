using JOX.Assistant.Models;
using JOX.Assistant.Interface;

namespace JOX.Assistant.Storage
{
    public class JOXStorage : IJOXStorage
    {
        private readonly ICosmosDbService _cosmosDbService;

        public JOXStorage(ICosmosDbService cosmosDbService)
        {
            _cosmosDbService = cosmosDbService;
        }

        public async Task<Conversation> GetConvesation(string id, string userId)
        {
            return await _cosmosDbService.GetConversationAsync(id, userId);
        }

        public async Task SaveConvesation(Conversation conversation, bool IsNew = false)
        {
            await _cosmosDbService.SaveConvesation(conversation, IsNew);
        }
    }
}
