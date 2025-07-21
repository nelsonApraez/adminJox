using System;
using System.Threading.Tasks;
using EventSourcingCore.Class;

namespace EventSourcingCore.Provider
{
    public interface IProviderSubscribe
    {

        Func<EventBusiness, Task> EvenBusiness { get; set; }

        string QueueName { get; set; }

        Task DeQueueAuto(string queue);

        Task DeQueueCustomToBusiness(string queue);

        Task<EventBusiness> DeQueueCustomEvent(string queue);

        Boolean Connected();

    }
}
