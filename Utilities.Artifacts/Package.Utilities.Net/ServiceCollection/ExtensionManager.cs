namespace Package.Utilities.Net
{
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Clase para agregar dinamicamente los repositorios de negocio
    /// </summary>
    public static class ExtensionManager
    {
        /// <summary>
        /// Agrega los repositorios genericos del proyecto desde el Exten Attribure BusinessAttribute
        /// </summary>
        /// <param name="services">Current services de aplicacion sobre el cual se inyecta los repositorios</param>
        /// <param name="interfaceParentType">Nombre de la interface padre que implementa los repositorios d negocio</param>
        /// <returns></returns>
        public static IServiceCollection AddServicesBusinessManager(this IServiceCollection services, string interfaceParentType)
        {
            var listClass = ExtensionReferencedAssemblies.GetTypeClassReference(typeof(BusinessAttribute), interfaceParentType);
            foreach (var item in listClass)
            {
                services.AddTransient(item.Key, item.Value);
            }
            return services;
        }

        /// <summary>
        /// Agrega los repositorios genericos del proyecto desde el Exten Attribure BusinessDaoAttribute
        /// </summary>
        /// <param name="services">Current services de aplicacion sobre el cual se inyecta los repositorios</param>
        /// <param name="interfaceParentType">Nombre de la interface padre que implementa los repositorios d negocio</param>
        /// <returns></returns>
        public static IServiceCollection AddServicesDaoBusinessManager(this IServiceCollection services, string interfaceParentType)
        {
            var listClass = ExtensionReferencedAssemblies.GetTypeClassReference(typeof(BusinessDaoAttribute), interfaceParentType);
            foreach (var item in listClass)
            {
                services.AddScoped(item.Key, item.Value);
            }
            return services;
        }

        /// <summary>
        /// Agrega los repositorios del proyecto desde el Exten Attribure BusinessRepositoryAttribute
        /// </summary>
        /// <param name="services">Current services de aplicacion sobre el cual se inyecta los repositorios</param>
        /// <param name="interfaceParentType">Nombre de la interface padre que implementa los repositorios d negocio</param>
        /// <returns></returns>
        public static IServiceCollection AddServicesRepositoriesManager(this IServiceCollection services, string interfaceParentType)
        {
            var listClass = ExtensionReferencedAssemblies.GetTypeClassReference(typeof(BusinessRepositoryAttribute), interfaceParentType);
            foreach (var item in listClass)
            {
                services.AddSingleton(item.Key);
            }
            return services;
        }
    }
}
