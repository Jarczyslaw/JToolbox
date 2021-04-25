using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.Misc.SysInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.SysInformation
{
    public class OSInformation : IDesktopExample
    {
        public string Title => "OS Information";

        public Task Run(IOutputInput outputInput)
        {
            outputInput.WriteLine("OS information:");
            outputInput.WriteLine(SystemInformation.GetOSInfo().PublicPropertiesToString());
            return Task.CompletedTask;
        }

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }
    }
}