using JToolbox.Core.TimeProvider;
using System;
using System.Net;
using System.Net.Sockets;

namespace JToolbox.Misc.TimeProviders.Ntp
{
    public class NtpUdpTimeProvider : TimeProviderBase
    {
        private readonly string ntpServer;

        public NtpUdpTimeProvider(string ntpServer)
        {
            this.ntpServer = ntpServer;
        }

        protected override DateTime Synchronize()
        {
            var ntpData = new byte[48];
            ntpData[0] = 0x1B;

            var addresses = Dns.GetHostEntry(ntpServer).AddressList;
            var ipEndPoint = new IPEndPoint(addresses[0], 123);
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp))
            {
                socket.ReceiveTimeout =
                    socket.SendTimeout = 3000;
                socket.Connect(ipEndPoint);
                socket.Send(ntpData);
                socket.Receive(ntpData);

                ulong intPart = (ulong)ntpData[40] << 24 | (ulong)ntpData[41] << 16 | (ulong)ntpData[42] << 8 | (ulong)ntpData[43];
                ulong fractPart = (ulong)ntpData[44] << 24 | (ulong)ntpData[45] << 16 | (ulong)ntpData[46] << 8 | (ulong)ntpData[47];

                var milliseconds = (intPart * 1000) + ((fractPart * 1000) / 0x100000000L);
                return (new DateTime(1900, 1, 1)).AddMilliseconds((long)milliseconds).ToLocalTime();
            }
        }
    }
}