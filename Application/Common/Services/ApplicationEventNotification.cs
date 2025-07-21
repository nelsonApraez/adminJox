using MediatR;

namespace Application.Common.Services
{
    public class ApplicationEventNotification<TApplicationEvent> : INotification where TApplicationEvent : class
    {
        public ApplicationEventNotification(TApplicationEvent objEvent)
        {
            ApplicationObjEvent = objEvent;
        }

        public TApplicationEvent ApplicationObjEvent { get; }
    }
}

