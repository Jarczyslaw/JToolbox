using JToolbox.WPF.Core.Awareness;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public class FileDragDropHelper : BaseDragDropHelper
    {
        public FileDragDropHelper(FrameworkElement frameworkElement)
            : base(frameworkElement)
        {
        }

        protected override string Key => DataFormats.FileDrop;

        protected override void DragStart(object sender, MouseEventArgs e)
        {
            if (frameworkElement.DataContext is IFileDragDropAware fileDragDropAware)
            {
                var files = fileDragDropAware.OnFileDrag();
                if (files?.Count > 0)
                {
                    DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.FileDrop, files.ToArray()), DragDropEffects.Move);
                    startPosition = null;
                }
            }
        }

        protected override void DropStart(object sender, DragEventArgs e)
        {
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files?.Length > 0 && frameworkElement.DataContext is IFileDragDropAware fileDragDropAware)
            {
                fileDragDropAware.OnFilesDrop(files.ToList());
            }
        }
    }
}