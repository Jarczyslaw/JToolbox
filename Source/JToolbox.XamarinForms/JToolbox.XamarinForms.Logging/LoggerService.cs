using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace JToolbox.XamarinForms.Logging
{
    public class LoggerService : ILoggerService
    {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly string noMessage = "No message provided";

        public static LoggerService CreateSingleConfiguration(string logPath)
        {
            return new LoggerService(new LoggerConfigBuilder(logPath).GetSingleConfiguration());
        }

        public static LoggerService CreateSplittedConfiguration(string logPath)
        {
            return new LoggerService(new LoggerConfigBuilder(logPath).GetSplittedConfiguration());
        }

        public LoggerService(LoggingConfiguration config)
        {
            LogManager.Configuration = config;
        }

        public void Debug(string message, params object[] args)
        {
            logger.Debug(message, args);
        }

        public void Error(Exception exception)
        {
            logger.Error(exception, noMessage);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            logger.Error(exception, message, args);
        }

        public void Error(string message, params object[] args)
        {
            logger.Error(message, args);
        }

        public void Fatal(Exception exception)
        {
            logger.Fatal(exception, noMessage);
        }

        public void Fatal(Exception exception, string message, params object[] args)
        {
            logger.Fatal(exception, message, args);
        }

        public void Fatal(string message, params object[] args)
        {
            logger.Fatal(message, args);
        }

        public void Info(string message, params object[] args)
        {
            logger.Info(message, args);
        }

        public void Trace(string message, params object[] args)
        {
            logger.Trace(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            logger.Warn(message, args);
        }

        public List<string> GetAllLogFiles()
        {
            var result = new List<string>();
            foreach (var target in LogManager.Configuration.AllTargets)
            {
                if (target is FileTarget fileTarget)
                {
                    result.Add(fileTarget.FileName.Render(new LogEventInfo()));
                }
            }
            return result;
        }

        public List<string> GetAllLogFolders()
        {
            return GetAllLogFiles().Select(f => Path.GetDirectoryName(f))
                .Distinct()
                .ToList();
        }
    }
}