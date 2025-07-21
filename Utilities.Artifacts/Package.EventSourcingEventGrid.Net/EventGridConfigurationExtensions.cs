using EventSourcingCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventSourcingEventGrid
{
    public static class EventGridConfigurationExtensions
    {
        /// <summary>
        /// Añade la interfaz IEventBusinessPublished para implementar la publicacion de eventos de negocio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options">opciones de configuracion para Event Grid</param>
        /// <returns></returns>
        public static IServiceCollection AddEventGridPublished(this IServiceCollection services,
           ConnectionsEGOptions options)
        {
            services.AddSingleton<IEventBusinessPublished>(new ProviderEGPublished(options));
            return services;
        }
    }
}
