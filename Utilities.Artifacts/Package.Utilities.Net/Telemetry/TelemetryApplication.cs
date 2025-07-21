using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Package.Utilities.Net.Telemetry
{
    /// <summary>
    /// 
    /// </summary>
    public class TelemetryApplication : ITelemetryApplication
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly TelemetryClient client;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="telemetryConfiguration"></param>
        public TelemetryApplication(TelemetryConfiguration telemetryConfiguration)
        {
            client = new TelemetryClient(telemetryConfiguration);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoTrackDependency"></param>
        public void TrackDependency(DependencyTelemetry infoTrackDependency)
        {
            client.TrackDependency(infoTrackDependency);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventTelemetry"></param>
        public void TrackEvent(EventTelemetry eventTelemetry)
        {
            client.TrackEvent(eventTelemetry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public void TrackEvent(string name)
        {
            client.TrackEvent(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        public void TrackEvent(string name, IDictionary<string, string> properties)
        {
            client.TrackEvent(name, properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        public void TrackEvent(string name, IDictionary<string, string> properties, IDictionary<string, double> metrics)
        {
            client.TrackEvent(name, properties, metrics);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionTelemetry"></param>
        public void TrackException(ExceptionTelemetry exceptionTelemetry)
        {
            client.TrackException(exceptionTelemetry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        public void TrackException(Exception ex)
        {
            client.TrackException(ex);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="properties"></param>
        public void TrackException(Exception ex, IDictionary<string, string> properties)
        {
            client.TrackException(ex, properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public void TrackMetric(string name, double value)
        {
            client.TrackMetric(name, value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metricTelemetry"></param>
        public void TrackMetric(MetricTelemetry metricTelemetry)
        {
            client.TrackMetric(metricTelemetry);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void TrackTrace(string message)
        {
            client.TrackTrace(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        public void TrackTrace(string message, IDictionary<string, string> properties)
        {
            client.TrackTrace(message, properties);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traceTelemetry"></param>
        public void TrackTrace(TraceTelemetry traceTelemetry)
        {
            client.TrackTrace(traceTelemetry);
        }
    }
}
