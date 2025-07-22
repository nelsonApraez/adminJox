

using JOX.Assistant.Models;

namespace JOX.Assistant.Interface
{
    public interface IJOXStorage
    {
        public Task<Conversation> GetConvesation(string id, string userId);
        public  Task SaveConvesation(Conversation conversation, bool IsNew=false);
    }
}
