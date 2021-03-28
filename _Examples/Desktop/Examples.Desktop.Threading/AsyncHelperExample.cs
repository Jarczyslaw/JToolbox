using Examples.Desktop.Base;
using System.Threading.Tasks;

namespace Examples.Desktop.Threading
{
    public class AsyncHelperExample : IDesktopExample
    {
        public string Title => "AsyncHelper";

        public Task CleanUp()
        {
            return Task.CompletedTask;
        }

        public Task Run(IOutputInput outputInput)
        {
            return Task.CompletedTask;
        }
    }
}