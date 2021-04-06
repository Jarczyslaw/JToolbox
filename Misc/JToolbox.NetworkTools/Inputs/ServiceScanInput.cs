using System.Collections.Generic;
using System.Net;

namespace JToolbox.NetworkTools.Inputs
{
    public class ServiceScanInput : InputBase
    {
        public List<IPAddress> Addresses { get; set; }

        public int Port { get; set; }
    }
}