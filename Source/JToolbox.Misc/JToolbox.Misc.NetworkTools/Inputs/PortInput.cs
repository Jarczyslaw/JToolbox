using System.Collections.Generic;
using System.Net;

namespace JToolbox.Misc.NetworkTools.Inputs
{
    public class PortInput : InputBase
    {
        public IPAddress Address { get; set; }
        public List<int> Ports { get; set; }
    }
}