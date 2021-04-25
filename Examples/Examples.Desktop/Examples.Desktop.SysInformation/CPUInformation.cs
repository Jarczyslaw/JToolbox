using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.Misc.SysInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.SysInformation
{
    public class CPUInformation : IDesktopExample
    {
        public string Title => "CPU Information";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("OS information:");
            outputInput.WriteLine(SystemInformation.GetCPUInfo().PublicPropertiesToString());
            return Task.CompletedTask;
        }
    }
}