using Azure.Messaging.ServiceBus;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.ServiceBus.Core;

namespace EventSourcingServiceBus.ServiceBusAccess
{
    internal static class SBBaseHelper
    {

        public static ServiceBusClient GetDatabase(string uri)
        {
            return string.IsNullOrEmpty(uri) ? null : new ServiceBusClient(uri);
        }

        public static ServiceBusClient GetDatabase(string uri, ServiceBusClientOptions options)
        {

            return string.IsNullOrEmpty(uri) ? null : new ServiceBusClient(uri, options);

        }

        public static MessageReceiver GetDatabaseReceived(string uri, string queuename)
        {

            return new MessageReceiver(uri, queuename, ReceiveMode.ReceiveAndDelete);

        }


    }
}
