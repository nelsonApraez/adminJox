using Application.Common.Models;
using Microsoft.Extensions.Logging;
using Moq;

namespace UnitTestsProject.Tests
{
    public static class ApplicationEventMockData
    {
        public static ILogger<Application.Common.Services.ApplicationEventService> GetILogger()
        {
            var mock = new Mock<ILogger<Application.Common.Services.ApplicationEventService>>();

            return mock.Object;
        }

        public static ApplicationEvent GetApplicationEvent()
        {
            var mock = new ApplicationEvent("Test");

            return mock;
        }


    }
}
