
using Nico.Assistant.Models;

namespace Nico.Assistant.Interface
{
    public interface IIARagService
    {
        Task<string> GetKnowloge(List<Chat> history, string msgdefault);
    }
}
