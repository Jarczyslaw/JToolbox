using System;
using System.IO;
using System.Net.Sockets;

namespace JToolbox.Misc.TimeProviders
{
    public class NtpTcpTimeProvider : TimeProviderBase
    {
        private readonly string ntpServer;

        public NtpTcpTimeProvider(string ntpServer)
        {
            this.ntpServer = ntpServer;
        }

        protected override DateTime Synchronize()
        {
            var tcp = new TcpClient(ntpServer, 13);
            string resp;
            using (var rdr = new StreamReader(tcp.GetStream()))
            {
                resp = rdr.ReadToEnd();
                var utc = resp.Substring(7, 17);
                return ParseDateTimeOffset(utc, "yy-MM-dd HH:mm:ss");
            }
        }
    }
}