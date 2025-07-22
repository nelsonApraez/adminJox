using JOX.Assistant.Models;

namespace JOX.Assistant.Socket
{
    public interface IHubClient
    {
        Task ReceiveMessage(ReceiveMessagePayload payload);
    }
}
