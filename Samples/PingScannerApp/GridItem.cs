using System.Net.NetworkInformation;

namespace PingScannerApp
{
    public class GridItem
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public IPStatus Status { get; set; }
    }
}