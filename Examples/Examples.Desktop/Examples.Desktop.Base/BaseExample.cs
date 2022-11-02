using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public abstract class BaseExample : IDesktopExample
    {
        public string Title => GetType().Name;

        public virtual Task CleanUp() => Task.CompletedTask;

        public abstract Task Run(IOutputInput outputInput);
    }
}