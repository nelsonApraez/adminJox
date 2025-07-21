using System;
using System.Collections.Generic;
using MediatR;

namespace Domain.Common
{
    public interface IHasDomainEvent
    {
        public List<DomainEvent> DomainEvents { get; set; }
    }

    public class DomainEvent : INotification
    {
        protected DomainEvent() => DateOccurred = DateTimeOffset.UtcNow;
        public DateTimeOffset DateOccurred { get; protected set; }

        public bool IsPublished { get; set; } = false;
        public string State { get; protected set; }
        public string IdReference { get; protected set; }
        public string Name { get; protected set; }

        public object Item { get; protected set; }
        public string ItemStr { get; protected set; }
    }
}
