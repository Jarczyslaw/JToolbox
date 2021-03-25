using System.Threading.Tasks;

namespace Examples.Desktop.Base
{
    public interface IDesktopExample
    {
        string Title { get; }
        Task Run(IOutputInput outputInput);
        Task CleanUp();
    }
}