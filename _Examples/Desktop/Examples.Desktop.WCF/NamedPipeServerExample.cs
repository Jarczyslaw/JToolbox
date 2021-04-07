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
    public class NamedPipeServerExample : ExampleBase, IDesktopExample
    {
        public string Title => "NamedPipe server";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public async Task Run(IOutputInput outputInput)
        {
            var configuration = Configurations.GetNamedPipeConfiguration();
            await StartServer(outputInput, configuration, null);
        }
    }
}
