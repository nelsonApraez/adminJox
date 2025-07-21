using Azure.Messaging.ServiceBus;
using EventSourcingCore;
using EventSourcingCore.Class;
using EventSourcingServiceBus.Provider;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace EventSourcingServiceBus.ServiceBus
{
    public static class ServiceBusConfigurationExtensions
    {

        /// <summary>
        /// Añade interfaz IEventBusinessPublished para publicar eventos de negocio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions">Opciones de configuracion para Service Bus</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBusPublished(this IServiceCollection services,
            ConnectionsSbOptions connectionOptions)
        {
            services.AddSingleton<IEventBusinessPublished>(new ProviderSbPublished(connectionOptions));
            return services;
        }

        /// <summary>
        /// Añade interfaz IEventBusinessPublished para publicar eventos de negocio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions">Opciones de configuracion para Service Bus</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBusPublished(this IServiceCollection services,
           ConnectionsSbOptions connectionOptions, ServiceBusClientOptions options)
        {
            services.AddSingleton<IEventBusinessPublished>(new ProviderSbPublished(connectionOptions, options));
            return services;
        }

        /// <summary>
        /// Añade interfaz IEventBusinessConsumer para recibir eventos de negocio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions">Opciones de configuracion para Service Bus</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBusSubscriber(this IServiceCollection services,
            ConnectionsSbOptions connectionOptions)
        {
            services.AddSingleton<IEventBusinessSubscriber>(new EventBusinessSubscriber(new ServicesBusSubscribe(connectionOptions)));
            return services;
        }

        /// <summary>
        /// Añade interfaz IEventBusinessConsumer para recibir eventos de negocio
        /// </summary>
        /// <param name="services"></param>
        /// <param name="connectionOptions">Opciones de configuracion para Service Bus</param>
        /// <param name="evenconsume">Evento hook que se ejecutara cuando se consuman los servicios</param>
        /// <returns></returns>
        public static IServiceCollection AddServiceBusSubscriber(this IServiceCollection services,
            ConnectionsSbOptions connectionOptions, Func<EventBusiness, Task> evenconsume)
        {
            services.AddSingleton<IEventBusinessSubscriber>(new EventBusinessSubscriber(new ServicesBusSubscribe(connectionOptions), evenconsume));
            return services;
        }

    }
}
