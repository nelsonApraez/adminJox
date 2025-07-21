namespace Package.Utilities.Net
{
    /// <summary>
    /// Interfaz para la extraccion de la configuraciones de la aplicación
    /// </summary>
    public interface IConfigurationApplication
    {
        /// <summary>
        /// Usuario de la session
        /// </summary>
        public string UserSession { get; }

        /// <summary>
        /// Endpoint Notifications
        /// </summary>
        public string EndpointNotifications { get; }

        /// <summary>
        /// SubscriptionKey Notifications
        /// </summary>
        public string SubscriptionKeyNotifications { get; }

        public string PathSecurityApi { get; }
    }
}
