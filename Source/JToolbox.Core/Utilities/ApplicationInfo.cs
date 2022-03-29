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
    }
}