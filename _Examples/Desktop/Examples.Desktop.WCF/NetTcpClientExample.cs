using Examples.Desktop.Base;
using JToolbox.WCF.ClientSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF
{
    public class NetTcpClientExample : IDesktopExample
    {
        public string Title => "NetTcp client";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            var address = Common.GetLocalAddress(outputInput);
            if (address == null)
            {
                return Task.CompletedTask;
            }

            var port = Common.GetPort(outputInput, 9989);
            if (port == 0)
            {
                return Task.CompletedTask;
            }

            var configuration = Configurations.GetNetTcpConfiguration(address.ToString(), port);
            using (var client = new Client<ITestService>(configuration))
            {
                client.Start();
                var message = outputInput.Read("Write message: ", "TestMessage");
                var result = client.Proxy.Ping(message);
                outputInput.WriteLine("Message from server: " + result);
            }
            return Task.CompletedTask;
        }
    }
}