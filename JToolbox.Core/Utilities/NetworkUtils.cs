using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace JToolbox.Core.Utilities
{
    public static class NetworkUtils
    {
        public static IPAddress GetLocalIPAddress()
        {
            return GetLocalIPAddresses().Find(a => a.AddressFamily == AddressFamily.InterNetwork);
        }

        public static List<IPAddress> GetLocalIPAddresses()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .ToList();
        }

        public static bool ConnectedToLocalNetwork()
        {
            return NetworkInterface.GetIsNetworkAvailable();
        }

        public static List<IPAddress> GetGatewayAddresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up && n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a?.AddressFamily == AddressFamily.InterNetwork && Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .ToList();
        }

        public static IPAddress GetGatewayAddress()
        {
            return GetGatewayAddresses().FirstOrDefault();
        }

        public static string GetHostName(IPAddress ipAddress)
        {
            try
            {
                return Dns.GetHostEntry(ipAddress)?.HostName;
            }
            catch (SocketException)
            {
                return null;
            }
        }

        public static IPAddress GetBroadcastAddress(IPAddress address, IPAddress mask)
        {
            var ipAddress = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            var ipMaskV4 = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
            var broadCastIpAddress = ipAddress | ~ipMaskV4;
            return new IPAddress(BitConverter.GetBytes(broadCastIpAddress));
        }

        public static IPAddress GetNetworkAddress(IPAddress address, IPAddress mask)
        {
            var ipAddress = BitConverter.ToUInt32(address.GetAddressBytes(), 0);
            var ipMaskV4 = BitConverter.ToUInt32(mask.GetAddressBytes(), 0);
            var networkIpAddress = ipAddress & ipMaskV4;
            return new IPAddress(BitConverter.GetBytes(networkIpAddress));
        }

        public static bool IsInSameSubnet(IPAddress address1, IPAddress address2, IPAddress subnetMask)
        {
            IPAddress network1 = GetNetworkAddress(address1, subnetMask);
            IPAddress network2 = GetNetworkAddress(address2, subnetMask);
            return network1.Equals(network2);
        }
    }
}