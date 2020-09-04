using JToolbox.WPF.Core.Base;

namespace WpfMvvmDragDrop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public TabsContextViewModel TabsContext { get; set; } = new TabsContextViewModel();
        public FilesContextViewModel FilesContext { get; set; } = new FilesContextViewModel();
    }
}