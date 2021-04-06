using Examples.Desktop.Base;
using JToolbox.WCF.ServerSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF
{
    public class NetTcpServerExample : IDesktopExample
    {
        public string Title => "NetTcp server";

        public Task CleanUp()
        {
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

            var service = new TestService(outputInput);
            var configuration = Configurations.GetNetTcpConfiguration(address.ToString(), port);
            var server = Server.CreateSingle<ITestService>(configuration, service);
            server.Start();
            outputInput.WriteLine("Server started at: " + configuration.ServiceAddress);
            await outputInput.Wait();
            server.Dispose();
        }
    }
}