using System;
using System.Net.NetworkInformation;

namespace JToolbox.Misc.NetworkTools.Results
{
    public class PingResult
    {
        public Exception LastException { get; internal set; }
        public PingReply Reply { get; internal set; }
    }
}