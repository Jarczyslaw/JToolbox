using Examples.Desktop.Base;
using JToolbox.Core.Utilities;
using System.Net;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class NetworkUtilities : IDesktopExample
    {
        public string Title => "Network utilities";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("Connected to local network: " + NetworkUtils.ConnectedToLocalNetwork());
            outputInput.WriteLine("Connected to internet: " + NetworkUtils.ConnectedToInternet());
            outputInput.PutLine();

            outputInput.WriteLine("Addresses:");
            foreach (var address in NetworkUtils.GetLocalIPAddresses())
            {
                outputInput.WriteLine($"\t{address}");
            }
            outputInput.PutLine();

            outputInput.WriteLine("Gateways:");
            foreach (var gateway in NetworkUtils.GetGatewayAddresses())
            {
                outputInput.WriteLine($"\t{gateway}");
            }
            outputInput.PutLine();

            outputInput.WriteLine("Host names:");
            foreach (var address in NetworkUtils.GetLocalIPAddresses())
            {
                outputInput.WriteLine($"\t{NetworkUtils.GetHostName(address)}");
            }
            outputInput.PutLine();

            var address1 = new IPAddress(new byte[] { 192, 168, 0, 1 });
            var address2 = new IPAddress(new byte[] { 192, 168, 1, 127 });
            var mask = new IPAddress(new byte[] { 255, 255, 255, 0 });
            outputInput.WriteLine($"Broadcast address for {address1}/{mask}: {NetworkUtils.GetBroadcastAddress(address1, mask)}");
            outputInput.WriteLine($"Network address for {address1}/{mask}: {NetworkUtils.GetNetworkAddress(address1, mask)}");
            outputInput.WriteLine($"Is {address1} and {address2} in the same subnet? (mask: {mask}): {NetworkUtils.IsInSameSubnet(address1, address2, mask)}");
            outputInput.WriteLine($"First address in subnet for {address2}/{mask}: {NetworkUtils.FirstAddressInSubnet(address2, mask)}");
            outputInput.WriteLine($"Last address in subnet for {address2}/{mask}: {NetworkUtils.LastAddressInSubnet(address2, mask)}");
            outputInput.PutLine();

            outputInput.WriteLine("Series of addresses:");
            foreach (var address in NetworkUtils.GetContinousAddressesInRange(new IPAddress(new byte[] { 192, 168, 1, 252 }), new IPAddress(new byte[] { 192, 168, 2, 3 })))
            {
                outputInput.WriteLine($"\t{address}");
            }
            outputInput.PutLine();

            outputInput.WriteLine("Active TCP connections ports:");
            foreach (var port in NetworkUtils.GetActiveTcpConnections())
            {
                outputInput.WriteLine($"\t{port}");
            }
            outputInput.WriteLine("Opened TCP ports:");
            foreach (var port in NetworkUtils.GetOpenTcpPorts())
            {
                outputInput.WriteLine($"\t{port}");
            }
            outputInput.WriteLine("Opened UDP ports:");
            foreach (var port in NetworkUtils.GetOpenUdpPorts())
            {
                outputInput.WriteLine($"\t{port}");
            }

            return Task.CompletedTask;
        }
    }
}