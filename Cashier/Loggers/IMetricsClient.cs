namespace Cashier.Loggers
{
    public interface IMetricsClient
    {
        public void LogEvent(string eventName, Dictionary<string, double> metrics = null, Dictionary<string, string> properties = null);
        public void LogException(string method,Exception exception);
    }
}
