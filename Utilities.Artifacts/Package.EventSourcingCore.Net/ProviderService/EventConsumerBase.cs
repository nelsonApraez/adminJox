using System;
using System.Threading.Tasks;
using EventSourcingCore.Class;
using EventSourcingCore.Provider;

namespace EventSourcingCore
{
    public class EventConsumerBase
    {
        private readonly IProviderSubscribe providerConsume;
        private readonly Boolean connected;

        public EventConsumerBase(IProviderSubscribe provider)
        {
            providerConsume = provider;
            providerConsume.EvenBusiness += ProcessEventConsume;
            connected = providerConsume.Connected();
        }


        /// <summary>
        /// Metodo que se debe sobre escribir en las las clases heredadas de negocio para que se procecen
        /// </summary>
        /// <param name="eventBusiness"></param>
        /// <returns></returns>
        protected virtual async Task ProcessEventConsume(EventBusiness eventBusiness)
        {
            await Task.Run(() => { Guid.NewGuid().ToString(); });
        }



        /// <summary>
        ///Se activa la cola de trabajo de acuerdo al nombre de la operacion de negocio y ejecuta el evento de negocio ProcessEventConsume 
        /// </summary>
        /// <returns></returns>
        protected async Task DeQueueAuto()
        {
            providerConsume.EvenBusiness += ProcessEventConsume;
            await providerConsume.DeQueueAuto(providerConsume.QueueName);
        }


        /// <summary>
        /// Ejecuta la carga de trabajo y desencola un elemento para ejecutar el evento de negocio ProcessEventConsume
        /// </summary>
        /// <returns></returns>
        protected async Task DeQueueCustomToBusiness()
        {
            await providerConsume.DeQueueCustomToBusiness(providerConsume.QueueName);
        }

        /// <summary>
        /// Ejecuta la carga de trabajo y desencola un elemento para retornar el evento de negocio dado
        /// </summary>
        /// <returns>Retorna el evento de negocio que necesita de acuerdo al topico suscrito</returns>
        protected async Task<EventBusiness> DeQueueCustomEvent()
        {
            return await providerConsume.DeQueueCustomEvent(providerConsume.QueueName);
        }

        public Boolean StatusConnect()
        {
            return connected;
        }

    }
}
