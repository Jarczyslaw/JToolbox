using System;
using System.Diagnostics;

namespace JToolbox.Core.Utilities
{
    public static class ApplicationInfo
    {
        public static bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public static bool IsDebugWithoutDebugger => IsDebug && !Debugger.IsAttached;

        public static bool IsDebugWithDebugger => IsDebug && Debugger.IsAttached;

        public static void ThrowIfDebugAppRunningWithoutDebugger()
        {
            if (IsDebugWithoutDebugger)
            {
                throw new DebugAppRunningWithoutDebuggerException();
            }
        }
    }

    public class DebugAppRunningWithoutDebuggerException : Exception
    {
        public DebugAppRunningWithoutDebuggerException()
            : base("Application built with DEBUG configuration can not be run without debugger attached")
        {
        }
    }
}