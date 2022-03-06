namespace JToolbox.Core.Utilities
{
    public static class ApplicationInfo
    {
        public static bool IsDebug()
        {
#if DEBUG
            return true;
#else
            return false;
#endif
        }
    }
}