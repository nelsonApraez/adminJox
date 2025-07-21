using Azure.Messaging.ServiceBus;
using EventSourcingCore.Provider;
using EventSourcingServiceBus.ServiceBusAccess;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcingServiceBus.Provider
{
    internal class ServicesBusPublished : IProviderPublished
    {
        private readonly ServiceBusClient connection;
        public ServicesBusPublished(string uri)
        {
            connection = SBBaseHelper.GetDatabase(uri);
        }

        public ServicesBusPublished(string uri, ServiceBusClientOptions options)
        {
            connection = SBBaseHelper.GetDatabase(uri, options);
        }

        public async Task QueueMessageAsync<T>(T model, string queue)
        {
            // create a sender for the queue 
            ServiceBusSender sender = connection?.CreateSender(queue);
            string messagestr = JsonSerializer.Serialize(model);
            // create a message that we can send
            ServiceBusMessage message = new ServiceBusMessage(messagestr);
            // send the message
            if (sender != null)
                await sender?.SendMessageAsync(message);
        }

        public bool Connected()
        {
            return connection != null;
        }

    }
}
