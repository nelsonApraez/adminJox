using Nico.Assistant.Models;

namespace Nico.Assistant.Socket
{
    public interface IHubClient
    {
        Task ReceiveMessage(ReceiveMessagePayload payload);
    }
}
