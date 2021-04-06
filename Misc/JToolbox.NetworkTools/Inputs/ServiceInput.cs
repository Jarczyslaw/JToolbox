using System.Net;

namespace JToolbox.NetworkTools.Inputs
{
    public class ServiceInput : InputBase
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
    }
}