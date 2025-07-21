using System;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.Common.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Services
{
    public class ApplicationEventService : IApplicationEventService
    {
        private readonly ILogger<ApplicationEventService> _logger;
        private readonly IPublisher _mediator;

        public ApplicationEventService(ILogger<ApplicationEventService> logger, IPublisher mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Publish(ApplicationEvent domainEvent)
        {
            _logger.LogInformation("Publishing application event. Event - {event}", domainEvent.GetType().Name);
            await _mediator.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent));
        }

        private static INotification GetNotificationCorrespondingToDomainEvent(ApplicationEvent domainEvent)
        {
            return (INotification)Activator.CreateInstance(
                typeof(ApplicationEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent);
        }
    }
}

