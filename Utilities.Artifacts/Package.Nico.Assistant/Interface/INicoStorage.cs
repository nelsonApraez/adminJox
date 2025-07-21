

using Nico.Assistant.Models;

namespace Nico.Assistant.Interface
{
    public interface INicoStorage
    {
        public Task<Conversation> GetConvesation(string id, string userId);
        public  Task SaveConvesation(Conversation conversation, bool IsNew=false);
    }
}
