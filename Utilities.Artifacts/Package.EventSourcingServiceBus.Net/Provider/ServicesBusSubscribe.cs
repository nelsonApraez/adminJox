using Azure.Messaging.ServiceBus;
using EventSourcingCore.Class;
using EventSourcingCore.Provider;
using EventSourcingServiceBus.ServiceBus;
using EventSourcingServiceBus.ServiceBusAccess;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventSourcingServiceBus.Provider
{
    public class ServicesBusSubscribe : IProviderSubscribe
    {
        private readonly ServiceBusClient connection;
        private readonly string uriservice;
        private readonly string suscriptionBase;
        public Func<EventBusiness, Task> EvenBusiness { get; set; }
        public string QueueName { get; set; }

        public ServicesBusSubscribe(ConnectionsSbOptions options)
        {
            QueueName = options.QueueName;
            uriservice = options.Url;
            suscriptionBase = options.Suscription;
            connection = string.IsNullOrEmpty(options.Url) ? null : SBBaseHelper.GetDatabase(options.Url);
        }


        public async Task DeQueueAuto(string queue)
        {
            ServiceBusProcessor processor = connection?.CreateProcessor(queue, suscriptionBase, new ServiceBusProcessorOptions());
            if (processor != null)
            {
                // add handler to process messages
                processor.ProcessMessageAsync += MessageHandler;

                // add handler to process any errors
                processor.ProcessErrorAsync += ErrorHandler;

                // start processing 
                await processor.StartProcessingAsync();
            }
        }

        /// <summary>
        /// Evento que desencola un mensaje base 
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        private async Task<string> DeQueueEventBase(string queue)
        {
            var receiver = SBBaseHelper.GetDatabaseReceived(this.uriservice, queue);
            var message = await receiver.ReceiveAsync();
            if (message != null)
            {
                return await Task.FromResult(message.Body.ToString());
            }
            else
            {
                return await Task.FromResult(string.Empty);
            }
        }

        /// <summary>
        /// Metodo que desencola mensaje y lo serializa el envento base de negocio
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>retorna evento de negocio para procesarlo por instancia externa</returns>
        public async Task<EventBusiness> DeQueueCustomEvent(string queue)
        {
            EventBusiness eventBusiness = JsonSerializer.Deserialize<EventBusiness>(await DeQueueEventBase(queue));
            return await Task.FromResult(eventBusiness);
        }

        /// <summary>
        /// metodo que desencola mensaje base y lo pasa al evento de negocio
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public async Task DeQueueCustomToBusiness(string queue)
        {
            await QueueReceived(await DeQueueEventBase(queue));
        }


        /// <summary>
        /// Procesa el mensaje recibido desde la queue
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            await QueueReceived(args.Message.Body.ToString());
            // complete the message. messages is deleted from the queue. 
            await args.CompleteMessageAsync(args.Message);
        }


        /// <summary>
        /// Funcion que retorna el evento de negocio con el valor recibido
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task QueueReceived(string message)
        {
            if (!EvenBusiness.Equals(null))
            {
                var obj_Serilized = JsonSerializer.Deserialize<EventBusiness>(message);
                await EvenBusiness(obj_Serilized);
            }
        }

        private async Task ErrorHandler(ProcessErrorEventArgs args)
        {
            await QueueReceived(args.Exception.Message);
        }

        public bool Connected()
        {
            return connection != null;
        }
    }
}
