using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.SysInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.SysInformation
{
    public class MemoryInformation : IDesktopExample
    {
        public string Title => "Memory Information";

        public Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("Memory information:");
            outputInput.WriteLine(SystemInformation.GetMemoryInfo().PublicPropertiesToString());
            return Task.CompletedTask;
        }

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }
    }
}
