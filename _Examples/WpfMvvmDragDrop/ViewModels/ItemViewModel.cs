using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Awareness.Args;
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

        public void OnDrag(DragDropArgs args)
        {
            Debug.WriteLine("Item OnDrag");
        }

        public void OnDrop(DragDropArgs args)
        {
            Debug.WriteLine("Item OnDrop, source: " + args.Source.GetType().Name);
        }
    }
}