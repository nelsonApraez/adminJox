using Azure;
using Azure.Messaging.EventGrid;
using EventSourcingCore;
using EventSourcingCore.Class;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcingEventGrid
{
    public class ProviderEGPublished : IEventBusinessPublished
    {
        private readonly EventGridPublisherClient connection;
        private readonly string connectionsQueue;

        public ProviderEGPublished(ConnectionsEGOptions options)
        {
            this.connectionsQueue = options.QueueName;
            this.connection = string.IsNullOrEmpty(options.Url) ? null : new EventGridPublisherClient(new Uri(options.Url), new AzureKeyCredential(options.KeyIdentity));
            connectionsQueue = string.Empty;
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
            string messagestr = JsonSerializer.Serialize(model);
            EventGridEvent message = new EventGridEvent(queue, "CompanyApp", "1.0", messagestr);
            if (connection != null)
                await connection?.SendEventAsync(message);
        }

        public async Task EventPublishedAsync(EventBusiness model)
        {
            await EventPublishedAsync(model, connectionsQueue);
        }
    }
}
