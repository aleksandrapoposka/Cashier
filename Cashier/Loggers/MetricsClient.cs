using Microsoft.ApplicationInsights;

namespace Cashier.Loggers
{
    public class MetricsClient : IMetricsClient
    {
        private readonly TelemetryClient _telemetryClient;
        public MetricsClient(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void LogEvent(string eventName, Dictionary<string, double> metrics = null, Dictionary<string, string> properties = null)
        {
            properties ??= new Dictionary<string, string>();

            _telemetryClient.TrackEvent(eventName, properties, metrics);
        }
        public void LogException(string method,Exception exception)
        {
            _telemetryClient.TrackException(exception, new Dictionary<string, string>() { { "EventName", "HandledException" }, { "HandledAt", method }, { "Message", exception.Message } });
        }
    }
}
