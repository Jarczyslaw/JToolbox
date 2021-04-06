using Examples.Desktop.Base;
using JToolbox.WCF.Common;
using JToolbox.WCF.ServerSide;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF
{
    public class NamedPipeServerExample : IDesktopExample
    {
        public string Title => "Named pipe server";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            var service = new TestService(outputInput);
            var configuration = Configurations.GetNamedPipeConfiguration();
            var server = Server.CreateSingle<ITestService>(configuration, service);
            server.Start();
            outputInput.WriteLine("Server started at: " + configuration.ServiceAddress);
            await outputInput.Wait();
            server.Dispose();
        }
    }
}
