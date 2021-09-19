namespace JToolbox.Misc.SysInformation
{
    public class MemoryInfo
    {
        public ulong FreePhysicalMemory { get; set; }
        public ulong FreeVirtualMemory { get; set; }
        public ulong TotalVirtualMemory { get; set; }
        public ulong TotalVisibleMemory { get; set; }
    }
}