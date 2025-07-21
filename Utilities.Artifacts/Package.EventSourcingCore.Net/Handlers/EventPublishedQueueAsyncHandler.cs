using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCore.Commands;
using MediatR;

namespace EventSourcingCore.Handlers
{
    public class EventPublishedQueueAsyncHandler : IRequestHandler<EventPublishedQueueAsync, string>
    {
        private readonly IEventBusinessPublished published;
        public EventPublishedQueueAsyncHandler(IEventBusinessPublished eventBusinessPublished)
        {
            published = eventBusinessPublished;
        }

        /// <summary>
        /// LLamado de Interfaz de conector de publicacion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(EventPublishedQueueAsync request, CancellationToken cancellationToken)
        {
            await published.EventPublishedAsync(request.model, request.queue);
            //se crea un identificador de tarea para fines de seguimiento
            return Guid.NewGuid().ToString();
        }
    }
}
