using System.Collections.Generic;
using System.Net;

namespace JToolbox.Misc.NetworkTools.Inputs
{
    public class PortScanInput : InputBase
    {
        public List<IPAddress> Addresses { get; set; }
        public List<int> Ports { get; set; }
    }
}