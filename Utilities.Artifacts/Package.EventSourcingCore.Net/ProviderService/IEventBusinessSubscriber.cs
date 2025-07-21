using System;
using System.Threading.Tasks;
using EventSourcingCore.Class;

namespace EventSourcingCore
{
    public interface IEventBusinessSubscriber
    {
        Func<EventBusiness, Task> EvenBusiness { get; set; }
        Task InitilizeConsumeAuto();
        Task InitilizeConsumeCustom();
        Task<EventBusiness> ReadConsumeCustom();
    }
}
