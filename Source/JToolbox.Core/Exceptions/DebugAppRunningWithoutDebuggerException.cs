using System;

namespace JToolbox.Core.Exceptions
{
    public class DebugAppRunningWithoutDebuggerException : Exception
    {
        public DebugAppRunningWithoutDebuggerException()
            : base("Application built with DEBUG configuration can not be run without debugger attached")
        {
        }
    }
}