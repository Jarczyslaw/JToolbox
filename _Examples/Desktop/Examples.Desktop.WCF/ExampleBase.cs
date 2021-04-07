using Examples.Desktop.Base;
using JToolbox.WCF.ClientSide;
using JToolbox.WCF.Common;
using JToolbox.WCF.ServerSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF
{
    public class ExampleBase
    {
        protected async Task StartServer(IOutputInput outputInput, BindingConfigurationBase bindingConfigurationBase, ServerConfiguration serverConfiguration)
        {
            var service = new TestService(outputInput);
            var server = Server.CreateSingle<ITestService>(bindingConfigurationBase, service, serverConfiguration);
            server.Start();
            outputInput.WriteLine("Server started at: " + bindingConfigurationBase.ServiceAddress);
            await outputInput.Wait();
            server.Dispose();
        }

        protected void StartClient(IOutputInput outputInput, BindingConfigurationBase bindingConfigurationBase)
        {
            using (var client = new Client<ITestService>(bindingConfigurationBase))
            {
                client.Start();
                var message = outputInput.Read("Write message: ", "TestMessage");
                var result = client.Proxy.Ping(message);
                outputInput.WriteLine("Message from server: " + result);
            }
        }
    }
}