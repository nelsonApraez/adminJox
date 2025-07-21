namespace EventSourcingServiceBus.ServiceBus
{
    public class ConnectionsSbOptions
    {
        /// <summary>
        /// Url de serivicio Service Bus Azure
        /// </summary>
        public string Url { get; set; }


        /// <summary>
        /// Nombre de queue
        /// </summary>
        public string QueueName { get; set; }

        /// <summary>
        /// Nombre de suscripcion queue
        /// </summary>
        public string Suscription { get; set; }

    }
}
