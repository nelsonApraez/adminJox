using System;
using System.Threading.Tasks;
using EventSourcingCore.Class;
using EventSourcingCore.Provider;

namespace EventSourcingCore
{
    public class EventBusinessSubscriber : EventConsumerBase, IEventBusinessSubscriber
    {
        public Func<EventBusiness, Task> EvenBusiness { get; set; }

        public EventBusinessSubscriber(IProviderSubscribe provider) : base(provider)
        {
        }

        public EventBusinessSubscriber(IProviderSubscribe provider, Func<EventBusiness, Task> eventconsumer) : base(provider)
        {
            EvenBusiness = eventconsumer;
        }

        /// <summary>
        /// este evento se ejecuta para desplegar los eventos de negocio
        /// </summary>
        /// <param name="eventBusiness"></param>
        /// <returns></returns>
        protected override async Task ProcessEventConsume(EventBusiness eventBusiness)
        {
            if (EvenBusiness != null)
            {
                await EvenBusiness(eventBusiness);
            }
        }

        /// <summary>
        /// Se ejecuta de forma indefinida los enventos de negocio
        /// </summary>
        /// <returns></returns>
        public async Task InitilizeConsumeAuto()
        {
            await base.DeQueueAuto();
        }

        /// <summary>
        /// Se inicia la ejecucion solo una vez del evento de negocio
        /// </summary>
        /// <returns></returns>
        public async Task InitilizeConsumeCustom()
        {
            await base.DeQueueCustomToBusiness();
        }

        /// <summary>
        /// se ejecuta el metodo y obtine un elemento de QueUe
        /// </summary>
        /// <returns></returns>
        public async Task<EventBusiness> ReadConsumeCustom()
        {
            return await base.DeQueueCustomEvent();
        }

    }
}
