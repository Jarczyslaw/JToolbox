using Examples.Desktop.Base;
using JToolbox.Misc.WCF.ServerSide;
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
            var serverConfiguration = new ServerConfiguration
            {
                IncludeExceptionDetailInFaults = true,
                CreateMexBinding = true,
            };
            await StartServer(outputInput, configuration, serverConfiguration);
        }
    }
}