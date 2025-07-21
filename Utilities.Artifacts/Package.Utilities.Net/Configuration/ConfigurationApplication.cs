namespace Package.Utilities.Net
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Clase para la extraccion de la configuraciones de la aplicación
    /// </summary>
    public class ConfigurationApplication : IConfigurationApplication
    {
        /// <summary>
        /// Instancia de Configuration Application Transversal
        /// </summary>
        private readonly IConfiguration configurationApplication;

        /// <summary>
        /// Instancia de Http Context
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;

        /// <summary>
        /// Constructor Configuration Application
        /// </summary>
        /// <param name="configuration">Instancia de Configuration Application Transversal</param>
        /// <param name="httpContextAccessor">Instancia de Http Context</param>
        public ConfigurationApplication(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            if (configuration != null)
            {
                configurationApplication = configuration;
            }

            if (httpContextAccessor != null)
            {
                this.httpContextAccessor = httpContextAccessor;
            }
        }

        /// <summary>
        /// Obtener el Usuario de la session
        /// </summary>
        public string UserSession
        {
            get
            {
                if (httpContextAccessor != null)
                {
                    var currentUser = httpContextAccessor.HttpContext.User;
                    return currentUser.FindFirst(ClaimsIdentity.DefaultNameClaimType)?.Value;
                }

                return string.Empty;
            }
        }

        public string EndpointNotifications => configurationApplication?.GetSection(Constants.ConfigurationApplication + nameof(EndpointNotifications)).Value;

        public string SubscriptionKeyNotifications => configurationApplication?.GetSection(Constants.ConfigurationApplication + nameof(SubscriptionKeyNotifications)).Value;

        public string PathSecurityApi => configurationApplication?.GetSection(Constants.ConfigurationApplication + nameof(PathSecurityApi)).Value;
    }
}
