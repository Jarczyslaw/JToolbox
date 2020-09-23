using JToolbox.WPF.Core.Awareness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public delegate void OnFileDrag(UiFileDragArgs args);

    public delegate void OnFileDrop(UiFileDropArgs args);

    public class FileDragDropHelper : BaseDragDropHelper
    {
        private readonly List<Type> fileDragSources;
        private readonly List<Type> fileDropTargets;

        public event OnFileDrag OnFileDrag;

        public event OnFileDrop OnFileDrop;

        public FileDragDropHelper(FrameworkElement frameworkElement, List<Type> fileDragSources, List<Type> fileDropTargets)
            : base(frameworkElement)
        {
            this.fileDragSources = fileDragSources;
            this.fileDropTargets = fileDropTargets;
        }

        protected override string Key => DataFormats.FileDrop;

        private void CallOnDragChain(UiFileDragArgs args)
        {
            OnFileDrag?.Invoke(args);
            if (args.Files?.Count > 0)
            {
                return;
            }

            if (args.Element.DataContext is IFileDragDropAware parentAware)
            {
                parentAware.OnFileDrag(args);
                if (args.Files?.Count > 0)
                {
                    return;
                }
            }

            if (frameworkElement.DataContext is IFileDragDropAware elementAware)
            {
                elementAware.OnFileDrag(args);
            }
        }

        private void CallOnDropChain(UiFileDropArgs args)
        {
            args.Handled = false;
            OnFileDrop?.Invoke(args);
            if (args.Handled)
            {
                return;
            }

            if (args.Element.DataContext is IFileDragDropAware parentAware)
            {
                parentAware.OnFilesDrop(args);
                if (args.Handled)
                {
                    return;
                }
            }

            if (frameworkElement.DataContext is IFileDragDropAware elementAware)
            {
                elementAware.OnFilesDrop(args);
            }
        }

        protected override void DragStart(object sender, MouseEventArgs e)
        {
            var source = e.OriginalSource as DependencyObject;
            if (Utils.FindParentOfTypes(source, fileDragSources) is FrameworkElement parent)
            {
                var args = new UiFileDragArgs
                {
                    Element = parent
                };
                CallOnDragChain(args);
                if (args.Files?.Count > 0)
                {
                    DragDrop.DoDragDrop(source, new DataObject(DataFormats.FileDrop, args.Files.ToArray()), DragDropEffects.Move);
                    startPosition = null;
                }
            }
        }

        protected override void DropStart(object sender, DragEventArgs e)
        {
            var target = e.OriginalSource as DependencyObject;
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files?.Length > 0 && Utils.FindParentOfTypes(target, fileDropTargets) is FrameworkElement parent)
            {
                var args = new UiFileDropArgs
                {
                    Files = files.ToList(),
                    Element = parent
                };
                CallOnDropChain(args);
            }
        }
    }
}