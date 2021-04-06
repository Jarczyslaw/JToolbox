using Examples.Desktop.Base;
using JToolbox.WCF.ClientSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF
{
    public class NamedPipeClientExample : IDesktopExample
    {
        public string Title => "Named pipe client";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            var configuration = Configurations.GetNamedPipeConfiguration();
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