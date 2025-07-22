
using JOX.Assistant.Models;

namespace JOX.Assistant.Interface
{
    public interface IJOXService
    {
        public Task ProcessMessageStart(Func<string, Task> fncallback, string text = "");
        public Task ProcessAndValidateConvesation(Conversation convesation, Func<ResponseJOX, Task> fncallback);
    }
}
