using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace JToolbox.NetworkTools.Inputs
{
    public class PingScanInput
    {
        public List<IPAddress> Addresses { get; set; }
        public int Timeout { get; set; } = 1000;
        public int Retries { get; set; } = 1;
    }
}