using NLog;
using ILogger = MPD.Electio.SDK.Interfaces.ILogger;

namespace MPD.Electio.SDK.Samples.Logging.NLog
{
    public class CustomSdkLogger : ILogger
    {
        private readonly Logger _logger;

        public CustomSdkLogger()
        {
            const string APP_NAME = "MyCustomAppName"; //supply this however you choose
            _logger = LogManager.GetLogger(APP_NAME);
        }

        public void Debug(string format, params object[] args)
        {
            _logger.Log(LogLevel.Debug, format, args);
        }

        public void Info(string format, params object[] args)
        {
            _logger.Log(LogLevel.Info, format, args);
        }

        public void Trace(string format, params object[] args)
        {
            _logger.Log(LogLevel.Trace, format, args);
        }

        public void Warn(string format, params object[] args)
        {
            _logger.Log(LogLevel.Warn, format, args);
        }

        public void Error(string format, params object[] args)
        {
            _logger.Log(LogLevel.Error, format, args);
        }

        public void Fatal(string format, params object[] args)
        {
            _logger.Log(LogLevel.Fatal, format, args);
        }
    }
}
