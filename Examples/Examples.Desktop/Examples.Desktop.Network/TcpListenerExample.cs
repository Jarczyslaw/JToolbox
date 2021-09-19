using Examples.Desktop.Base;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Examples.Desktop.Network
{
    public class TcpListenerExample : IDesktopExample
    {
        private TcpListener listener;
        public string Title => "TCP Listener";

        public Task CleanUp()
        {
            listener?.Stop();
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            var address = Common.GetLocalAddress(outputInput);
            if (address == null)
            {
                return;
            }

            var port = Common.GetPort(outputInput, 9989);
            if (port == 0)
            {
                return;
            }

            listener = new TcpListener(address, port);
            listener.Start();
            outputInput.WriteLine($"Listener started at {address}:{port}");
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                outputInput.WriteLine("Client connected from: " + ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString());
            }
        }
    }
}