using Serilog;
using Serilog.Events;
using SolforbTest.WebClient.Config.Consts;
using System.Globalization;

namespace SolforbTest.WebClient.Config
{
    public class LogConfig
    {
        private const string DefaultFileName = "SolforbTest.WebClientLog-.txt";

        public static Serilog.ILogger GetLoggerConfiguration(IConfiguration configuration)
        {
            string logFileName = configuration[ConfigurationKeys.LogFileNameKey] ?? DefaultFileName;
            return new LoggerConfiguration().MinimumLevel
                .Override("Microsoft", LogEventLevel.Information)
                .WriteTo.File(
                    logFileName,
                    formatProvider: new CultureInfo("ru-RU"),
                    rollingInterval: RollingInterval.Day
                )
                .CreateLogger();
        }
    }
}
