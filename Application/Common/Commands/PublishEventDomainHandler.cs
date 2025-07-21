using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Commands
{
    public record PublishEventDomainCommand(INotification Notification) : IRequest<Unit>;
    public class PublishEventDomainHandler : IRequestHandler<PublishEventDomainCommand, Unit>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PublishEventDomainHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task<Unit> Handle(PublishEventDomainCommand request, CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                using var scope = _serviceScopeFactory.CreateScope();
                var mediator = scope.ServiceProvider.GetService<IMediator>();
                await mediator.Publish(request.Notification, cancellationToken);
            }, cancellationToken);
            return Task.FromResult(Unit.Value);
        }
    }
}
