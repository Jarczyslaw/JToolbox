using System.Net;

namespace JToolbox.NetworkTools
{
    public class PortResult
    {
        public IPAddress Address { get; set; }
        public int Port { get; set; }
        public bool IsOpen { get; set; }
    }
}