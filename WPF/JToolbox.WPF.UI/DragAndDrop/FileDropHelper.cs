using JToolbox.WPF.Core.Awareness;
using System.Linq;
using System.Windows;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public class FileDropHelper
    {
        private readonly FrameworkElement frameworkElement;

        public FileDropHelper(FrameworkElement frameworkElement)
        {
            this.frameworkElement = frameworkElement;
            frameworkElement.AllowDrop = true;

            PinEvents();
        }

        public void PinEvents()
        {
            frameworkElement.Drop += Drop;
        }

        public void UnpinEvents()
        {
            frameworkElement.Drop -= Drop;
        }

        private void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = e.Data.GetData(DataFormats.FileDrop) as string[];
                if (files?.Length > 0 && frameworkElement.DataContext is IFileDropAware fileDropAware)
                {
                    fileDropAware.OnFileDrop(files.ToList());
                }
            }
        }
    }
}