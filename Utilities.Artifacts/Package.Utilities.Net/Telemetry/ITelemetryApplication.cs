namespace Package.Utilities.Net.Telemetry
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ApplicationInsights.DataContracts;

    /// <summary>
    /// 
    /// </summary>
    public interface ITelemetryApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="infoTrackDependency"></param>
        void TrackDependency(DependencyTelemetry infoTrackDependency);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventTelemetry"></param>
        void TrackEvent(EventTelemetry eventTelemetry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        void TrackEvent(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        void TrackEvent(string name, IDictionary<string, string> properties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="properties"></param>
        /// <param name="metrics"></param>
        void TrackEvent(string name, IDictionary<string, string> properties, IDictionary<string, double> metrics);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exceptionTelemetry"></param>
        void TrackException(ExceptionTelemetry exceptionTelemetry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        void TrackException(Exception ex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="properties"></param>
        void TrackException(Exception ex, IDictionary<string, string> properties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="metricTelemetry"></param>
        void TrackMetric(MetricTelemetry metricTelemetry);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        void TrackMetric(string name, double value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        void TrackTrace(string message);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="properties"></param>
        void TrackTrace(string message, IDictionary<string, string> properties);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="traceTelemetry"></param>
        void TrackTrace(TraceTelemetry traceTelemetry);
    }
}
