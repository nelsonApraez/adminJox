using System.Threading.Tasks;
using EventSourcingCore.Class;

namespace EventSourcingCore
{
    public interface IEventBusinessPublished
    {
        public Task EventPublishedAsync<T>(T model, string idOperation, string idUser, string queue);
        public Task EventPublishedAsync(EventBusiness model, string queue);
        public Task EventPublishedAsync(EventBusiness model);
    }
}
