using NLog;

namespace APITests.APICore
{
    public class LoggerHelper
    {
        private readonly Logger _logger;
        public LoggerHelper()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogHttpRequest(string method, string url, HttpResponseMessage response, string requestBody = null)
        {
            LogInfo($"HTTP Method: {method}");
            LogInfo($"Request URL: {url}");
            if (requestBody != null)
            {
                LogInfo($"Request Body: {requestBody}");
            }
            LogInfo($"Expected Response: Success Status Code");
            LogInfo($"Actual Response: {response.StatusCode} {response.Content.ReadAsStringAsync().Result}");
        }
    }
}