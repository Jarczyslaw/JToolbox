using JToolbox.WPF.Core.Awareness;
using JToolbox.WPF.Core.Awareness.Args;
using JToolbox.WPF.Core.Base;

namespace Examples.Desktop.WPFDragDrop.ViewModels
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
            EventLogs.AddWithClassName("OnDrag");
        }

        public void OnDrop(DragDropArgs args)
        {
            EventLogs.AddWithClassName("OnDrop, source: " + args.Source.GetType().Name);
        }
    }
}