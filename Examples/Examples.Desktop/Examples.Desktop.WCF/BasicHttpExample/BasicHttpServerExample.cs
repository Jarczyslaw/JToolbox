﻿using Examples.Desktop.Base;
using JToolbox.Misc.WCF.ServerSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF.BasicHttpExample
{
    public class BasicHttpServerExample : ExampleBase, IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "BasicHttp server";

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

            var port = Common.GetPort(outputInput, 9988);
            if (port == 0)
            {
                return;
            }

            var configuration = Configurations.GetBasicHttpConfiguration(address.ToString(), port);
            var serverConfiguration = new ServerConfiguration
            {
                IncludeExceptionDetailInFaults = true,
                CreateMexBinding = true
            };
            await StartServer(outputInput, configuration, serverConfiguration);
        }
    }
}