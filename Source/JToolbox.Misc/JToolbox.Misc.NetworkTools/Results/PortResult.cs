using System;

namespace JToolbox.Misc.NetworkTools.Results
{
    public class PortResult
    {
        public bool IsOpen { get; internal set; }
        public Exception LastException { get; internal set; }
        public int Port { get; internal set; }
    }
}