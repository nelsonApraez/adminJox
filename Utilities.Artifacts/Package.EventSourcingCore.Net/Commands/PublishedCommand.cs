using EventSourcingCore.Class;
using MediatR;

namespace EventSourcingCore.Commands
{
    /// <summary>
    /// Publica un  modelo de negocio al provider de Queue
    /// </summary>
    public record EventPublishedAsync(EventBusiness model) : IRequest<string>;


    /// <summary>
    /// Publica un  modelo de negocio al provider de Queue por la suscripcion indicada
    /// </summary>
    public record EventPublishedQueueAsync(EventBusiness model, string queue) : IRequest<string>;
}
