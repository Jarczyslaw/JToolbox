using System.Collections.Generic;
using System.Net;

namespace JToolbox.Misc.NetworkTools.Inputs
{
    public class PingScanInput : InputBase
    {
        public List<IPAddress> Addresses { get; set; }
    }
}