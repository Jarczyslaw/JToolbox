using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public interface IDesktopExample
    {
        string CustomActionTitle { get; }

        string Title { get; }

        Task CleanUp();

        void CustomAction();

        Task Run(IOutputInput outputInput);
    }
}