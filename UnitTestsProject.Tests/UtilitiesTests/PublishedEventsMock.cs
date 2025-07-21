using System;
using System.Threading.Tasks;
using EventSourcingCore;
using EventSourcingCore.Class;

namespace UnitTestsProject.Tests.UtilitiesTests
{
    public class PublishedEventsMock : IEventBusinessPublished
    {
        public async Task EventPublishedAsync<T>(T model, string idOperation, string idUser, string queue)
        {
            await Task.FromResult(Guid.NewGuid().ToString());
        }

        public async Task EventPublishedAsync(EventBusiness model, string queue)
        {
            await Task.FromResult(Guid.NewGuid().ToString());
        }

        public async Task EventPublishedAsync(EventBusiness model)
        {
            await Task.FromResult(Guid.NewGuid().ToString());
        }
    }
}
