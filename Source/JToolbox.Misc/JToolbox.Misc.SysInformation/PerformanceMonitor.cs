using System.Diagnostics;

namespace JToolbox.Misc.SysInformation
{
    public static class PerformanceMonitor
    {
        private static readonly PerformanceCounter cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        private static readonly PerformanceCounter memoryCounter = new PerformanceCounter("Memory", "Available MBytes");

        static PerformanceMonitor()
        {
            cpuCounter.NextValue();
        }

        public static float GetAvailableMemoryMB()
        {
            return memoryCounter.NextValue();
        }

        public static float GetCpuUsage()
        {
            return cpuCounter.NextValue();
        }
    }
}