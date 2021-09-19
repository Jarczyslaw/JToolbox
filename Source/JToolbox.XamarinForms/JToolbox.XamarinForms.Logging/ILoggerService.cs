using System;
using System.Collections.Generic;

namespace JToolbox.XamarinForms.Logging
{
    public interface ILoggerService
    {
        void Debug(string message, params object[] args);

        void Error(Exception exception);

        void Error(Exception exception, string message, params object[] args);

        void Error(string message, params object[] args);

        void Fatal(Exception exception);

        void Fatal(Exception exception, string message, params object[] args);

        void Fatal(string message, params object[] args);

        List<string> GetAllLogFiles();

        List<string> GetAllLogFolders();

        void Info(string message, params object[] args);

        void Trace(string message, params object[] args);

        void Warn(string message, params object[] args);
    }
}