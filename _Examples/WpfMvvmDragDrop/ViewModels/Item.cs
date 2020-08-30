using JToolbox.WPF.Core.Base;

namespace WpfMvvmDragDrop.ViewModels
{
    public class Item : BaseViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }
    }
}