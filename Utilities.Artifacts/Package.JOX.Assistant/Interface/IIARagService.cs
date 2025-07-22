
using JOX.Assistant.Models;

namespace JOX.Assistant.Interface
{
    public interface IIARagService
    {
        Task<string> GetKnowloge(List<Chat> history, string msgdefault);
    }
}
