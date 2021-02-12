using NLog;
using NLog.Config;
using NLog.Targets;
using System.IO;

namespace JToolbox.XamarinForms.Logging
{
    public class LoggerConfigBuilder
    {
        private readonly string logsPath;
        private readonly LoggingConfiguration config;

        public LoggerConfigBuilder(string logsPath)
        {
            this.logsPath = logsPath;
            config = new LoggingConfiguration();
        }

        public LoggingConfiguration AddTargetFile(string targetName, string fileName)
        {
            var target = new FileTarget()
            {
                Name = targetName,
                FileName = Path.Combine(logsPath, $"{fileName}.txt"),
                ArchiveFileName = Path.Combine(logsPath, fileName + "{#}.txt"),
                ArchiveAboveSize = 10485760,
                ArchiveNumbering = ArchiveNumberingMode.Sequence,
                MaxArchiveFiles = 10,
                ConcurrentWrites = true,
                KeepFileOpen = false,
                Layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss.fff} - [${level:uppercase=true}]: ${message} ${onexception:${newline}${exception:format=ToString}}"
            };
            config.AddTarget(target);
            return config;
        }

        public LoggingConfiguration AddRule(LogLevel minLevel, LogLevel maxLevel, string targetName, string loggerNamePattern)
        {
            config.AddRule(minLevel, maxLevel, targetName, loggerNamePattern);
            return config;
        }

        public LoggingConfiguration GetSingleConfiguration()
        {
            const string targetName = "CommonLog";
            AddTargetFile(targetName, "log");
            config.AddRuleForAllLevels(targetName);
            return config;
        }

        public LoggingConfiguration GetSplittedConfiguration()
        {
            const string infoTargetName = "InfoLog";
            AddTargetFile(infoTargetName, "info");
            AddRule(LogLevel.Trace, LogLevel.Info, infoTargetName, "*");

            const string errorTargetName = "ErrorLog";
            AddTargetFile(errorTargetName, "error");
            AddRule(LogLevel.Warn, LogLevel.Fatal, errorTargetName, "*");

            return config;
        }
    }
}