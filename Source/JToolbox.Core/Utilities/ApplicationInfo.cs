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
    }
}