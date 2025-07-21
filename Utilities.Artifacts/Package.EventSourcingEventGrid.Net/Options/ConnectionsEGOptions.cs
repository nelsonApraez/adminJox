namespace EventSourcingEventGrid
{
    public class ConnectionsEGOptions
    {

        /// <summary>
        /// Url de serivicio de Event Grid
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Key identity de azure credentials
        /// </summary>
        public string KeyIdentity { get; set; }

        /// <summary>
        /// Nombre de QueUe de servicio
        /// </summary>
        public string QueueName { get; set; }
    }
}
