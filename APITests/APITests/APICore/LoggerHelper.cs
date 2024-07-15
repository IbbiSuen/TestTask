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
    }
}
