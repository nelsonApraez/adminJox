using System.Threading.Tasks;
using Application.Common.Models;

namespace Application.Common.Interfaces
{
    public interface IApplicationEventService
    {
        Task Publish(ApplicationEvent domainEvent);
    }
}

