using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class PortScanner
    {
        public async Task<bool> IsPortOpen(IPAddress address, int port, int timeout)
        {
            try
            {
                using (var client = new TcpClient())
                {
                    client.SendTimeout =
                        client.ReceiveTimeout = timeout;
                    await client.ConnectAsync(address, port);
                    return client.Connected;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}