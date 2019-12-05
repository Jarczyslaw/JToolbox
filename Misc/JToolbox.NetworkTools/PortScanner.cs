using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace JToolbox.NetworkTools
{
    public class PortScanner
    {
        public async Task<PortResult> IsPortOpen(PortInput input)
        {
            var isOpen = false;
            for (int i = 0; i < input.Retries; i++)
            {

            }
            return new PortResult
            {
                IsOpen = isOpen,
                Port = input.Port
            };
            try
            {
                using (var client = new TcpClient())
                {
                    client.SendTimeout =
                        client.ReceiveTimeout = input.Timeout;
                    await client.ConnectAsync(input.Address, input.Port);
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}