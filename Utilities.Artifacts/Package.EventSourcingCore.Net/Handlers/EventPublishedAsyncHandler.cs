using System;
using System.Threading;
using System.Threading.Tasks;
using EventSourcingCore.Commands;
using MediatR;

namespace EventSourcingCore.Handlers
{
    public class EventPublishedAsyncHandler : IRequestHandler<EventPublishedAsync, string>
    {
        private readonly IEventBusinessPublished published;
        public EventPublishedAsyncHandler(IEventBusinessPublished eventBusinessPublished)
        {
            published = eventBusinessPublished;
        }


        /// <summary>
        /// publicacion al conector de event sourcing 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<string> Handle(EventPublishedAsync request, CancellationToken cancellationToken)
        {
            await published.EventPublishedAsync(request.model);
            //se crea un identificador de tarea para fines de seguimiento
            return Guid.NewGuid().ToString();
        }
    }
}
