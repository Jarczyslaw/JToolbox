﻿using Examples.Desktop.Base;
using JToolbox.Misc.WCF.ServerSide;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF.NamedPipeExample
{
    public class NamedPipeServerExample : ExampleBase, IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "NamedPipe server";

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
            var configuration = Configurations.GetNamedPipeConfiguration();
            var serverConfiguration = new ServerConfiguration
            {
                IncludeExceptionDetailInFaults = true,
                CreateMexBinding = true,
            };
            await StartServer(outputInput, configuration, serverConfiguration);
        }
    }
}