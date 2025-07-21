using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.DataContracts;
using Package.Utilities.Net.Telemetry;

namespace UnitTestsProject.Tests.UtilitiesTests
{
    public class TelemetryApplicationMock : ITelemetryApplication
    {
        public void TrackDependency(DependencyTelemetry infoTrackDependency)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackEvent(EventTelemetry eventTelemetry)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackEvent(string name)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackEvent(string name, IDictionary<string, string> properties, IDictionary<string, double> metrics)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackException(ExceptionTelemetry exceptionTelemetry)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackException(Exception ex)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackException(Exception ex, IDictionary<string, string> properties)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackMetric(MetricTelemetry metricTelemetry)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackMetric(string name, double value)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackTrace(string message)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            Guid.NewGuid().ToString();
        }

        public void TrackTrace(TraceTelemetry traceTelemetry)
        {
            Guid.NewGuid().ToString();
        }
    }
}
