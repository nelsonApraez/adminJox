
using Nico.Assistant.Models;

namespace Nico.Assistant.Interface
{
    public interface INicoService
    {
        public Task ProcessMessageStart(Func<string, Task> fncallback, string text = "");
        public Task ProcessAndValidateConvesation(Conversation convesation, Func<ResponseNico, Task> fncallback);
    }
}
