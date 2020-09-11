using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Base;
using System.Diagnostics;

namespace WpfMvvmDragDrop.ViewModels
{
    public class ItemViewModel : BaseViewModel, IDragDropAware
    {
        private string name;

        public string Name
        {
            get => name;
            set => Set(ref name, value);
        }

        public void OnDrag(object source)
        {
            Debug.WriteLine("Item OnDrag");
        }

        public void OnDrop(object source, object target)
        {
            Debug.WriteLine("Item OnDrop, source: " + source.GetType().Name);
        }
    }
}