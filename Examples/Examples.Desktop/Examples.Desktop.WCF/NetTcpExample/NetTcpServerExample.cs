﻿using Examples.Desktop.Base;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF.NetTcpExample
{
    public class NetTcpServerExample : ExampleBase, IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "NetTcp server";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public void CustomAction()
        {
            throw new System.NotImplementedException();
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

            var configuration = Configurations.GetNetTcpConfiguration(address.ToString(), port);
            await StartServer(outputInput, configuration, null);
        }
    }
}