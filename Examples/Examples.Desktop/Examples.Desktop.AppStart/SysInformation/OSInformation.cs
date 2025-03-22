using Examples.Desktop.Base;
using JToolbox.Core.Extensions;
using JToolbox.Misc.SysInformation;
using System.Threading.Tasks;

namespace Examples.Desktop.AppStart.SysInformation
{
    public class OSInformation : IDesktopExample
    {
        public string CustomActionTitle => null;

        public string Group => "SysInformation";

        public string Title => "OS";

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
            outputInput.WriteLine("OS information:");
            outputInput.WriteLine(SystemInformation.GetOSInfo().PublicPropertiesToString());
            return Task.CompletedTask;
        }
    }
}