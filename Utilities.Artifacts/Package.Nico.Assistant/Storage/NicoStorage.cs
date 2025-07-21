using Nico.Assistant.Interface;
using Nico.Assistant.Models;


namespace Nico.Assistant.Storage
{
    public class NicoStorage: INicoStorage
    {
        private readonly ICosmosDbService _repository;

        public NicoStorage(ICosmosDbService repository)
        {
            this._repository = repository;
        }

        public async Task<Conversation> GetConvesation(string id, string userId)
        {
            return await _repository.GetConversationAsync(id, userId);
        }

        public async Task SaveConvesation(Conversation conversation, bool isNew = false)
        {
            await _repository.SaveConvesation(conversation,isNew);
        }
    }
}
