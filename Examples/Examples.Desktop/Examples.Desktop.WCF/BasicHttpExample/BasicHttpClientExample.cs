﻿using Examples.Desktop.Base;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF.BasicHttpExample
{
    public class BasicHttpClientExample : ExampleBase, IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Title => "BasicHttp client";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public void CustomAction()
        {
            throw new System.NotImplementedException();
        }

        public Task Run(IOutputInput outputInput)
        {
            var address = Common.GetLocalAddress(outputInput);
            if (address == null)
            {
                return Task.CompletedTask;
            }

            var port = Common.GetPort(outputInput, 9988);
            if (port == 0)
            {
                return Task.CompletedTask;
            }

            var configuration = Configurations.GetBasicHttpConfiguration(address.ToString(), port);
            StartClient(outputInput, configuration);
            return Task.CompletedTask;
        }
    }
}