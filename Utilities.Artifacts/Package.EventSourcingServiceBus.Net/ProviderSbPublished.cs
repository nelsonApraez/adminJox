using Azure.Messaging.ServiceBus;
using EventSourcingCore;
using EventSourcingCore.Class;
using EventSourcingCore.Provider;
using EventSourcingServiceBus.Provider;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcingServiceBus.ServiceBus
{
    public class ProviderSbPublished : IEventBusinessPublished
    {

        private readonly IProviderPublished providerQueue;
        private readonly String connectionsQueue;

        public ProviderSbPublished(ConnectionsSbOptions connectionOptions)
        {
            this.connectionsQueue = connectionOptions.QueueName;
            this.providerQueue = new ServicesBusPublished(connectionOptions.Url);
        }

        public ProviderSbPublished(ConnectionsSbOptions connectionOptions, ServiceBusClientOptions options)
        {
            connectionsQueue = connectionOptions.QueueName;
            providerQueue = new ServicesBusPublished(connectionOptions.Url, options);
        }

        public async Task EventPublishedAsync<T>(T model, string idOperation, string idUser, string queue)
        {
            await EventPublishedAsync(new EventBusiness()
            {
                IdUser = idUser,
                IdOperation = idOperation,
                Data = JsonSerializer.Serialize(model)
            }, queue);
        }

        public async Task EventPublishedAsync(EventBusiness model, string queue)
        {
            await providerQueue?.QueueMessageAsync(model, queue);
        }

        public async Task EventPublishedAsync(EventBusiness model)
        {
            await EventPublishedAsync(model, connectionsQueue);
        }
    }
}
