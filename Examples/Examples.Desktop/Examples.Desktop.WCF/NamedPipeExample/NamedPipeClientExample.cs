using Examples.Desktop.Base;
using System.Threading.Tasks;

namespace Examples.Desktop.WCF.NamedPipeExample
{
    public class NamedPipeClientExample : ExampleBase, IDesktopExample
    {
        public string Title => "NamedPipe client";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            var configuration = Configurations.GetNamedPipeConfiguration();
            StartClient(outputInput, configuration);
            return Task.CompletedTask;
        }
    }
}