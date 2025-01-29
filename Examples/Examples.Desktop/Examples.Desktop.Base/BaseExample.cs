using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public abstract class BaseExample : IDesktopExample
    {
        public virtual string CustomActionTitle => null;

        public string Title => GetType().Name;

        public virtual Task CleanUp() => Task.CompletedTask;

        public virtual void CustomAction()
        {
            throw new System.NotImplementedException();
        }

        public abstract Task Run(IOutputInput outputInput);
    }
}