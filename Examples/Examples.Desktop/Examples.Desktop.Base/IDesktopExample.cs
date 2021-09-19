using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public interface IDesktopExample
    {
        string Title { get; }

        Task CleanUp();

        Task Run(IOutputInput outputInput);
    }
}