using System.Collections.Generic;
using System.Net;

namespace JToolbox.NetworkTools.Inputs
{
    public class PortScanInput
    {
        public List<IPAddress> Addresses { get; set; }
        public List<int> Ports { get; set; }
        public int Timeout { get; set; } = 200;
        public int Retries { get; set; } = 1;
    }
}