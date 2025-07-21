using System;
namespace Application.Common.Models
{
    public class ApplicationEvent
    {

        public ApplicationEvent(object item) : this() => ObjItem = item;

        protected ApplicationEvent() => DateOccurred = DateTimeOffset.UtcNow;

        public object ObjItem { get; protected set; }

        public DateTimeOffset DateOccurred { get; protected set; }
    }
}

