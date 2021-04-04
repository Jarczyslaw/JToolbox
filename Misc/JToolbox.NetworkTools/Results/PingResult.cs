using System;
using System.Net;
using System.Net.NetworkInformation;

namespace JToolbox.NetworkTools.Results
{
    public class PingResult
    {
        public PingReply Reply { get; internal set; }
        public Exception LastException { get; internal set; }
    }
}