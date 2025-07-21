using System;
using System.Threading.Tasks;

namespace EventSourcingCore.Provider
{
    public interface IProviderPublished
    {
        public Task QueueMessageAsync<T>(T model, string queue);

        Boolean Connected();
    }
}
