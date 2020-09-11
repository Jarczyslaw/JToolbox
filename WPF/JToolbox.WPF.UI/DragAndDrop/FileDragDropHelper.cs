using JToolbox.WPF.Core.Awareness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public class FileDragDropHelper : BaseDragDropHelper
    {
        private readonly List<Type> fileDragSources;
        private readonly List<Type> fileDropTargets;

        public FileDragDropHelper(FrameworkElement frameworkElement)
            : this(frameworkElement, null, null)
        {
        }

        public FileDragDropHelper(FrameworkElement frameworkElement, List<Type> fileDragSources, List<Type> fileDropTargets)
            : base(frameworkElement)
        {
            this.fileDragSources = fileDragSources;
            this.fileDropTargets = fileDropTargets;
        }

        protected override string Key => DataFormats.FileDrop;

        protected override void DragStart(object sender, MouseEventArgs e)
        {
            var sourceElement = FindElement(e.OriginalSource, fileDragSources);
            if (sourceElement?.DataContext is IFileDragDropAware fileDragDropAware)
            {
                var files = fileDragDropAware.OnFileDrag();
                if (files?.Count > 0)
                {
                    DragDrop.DoDragDrop(sourceElement, new DataObject(DataFormats.FileDrop, files.ToArray()), DragDropEffects.Move);
                    startPosition = null;
                }
            }
        }

        protected override void DropStart(object sender, DragEventArgs e)
        {
            var sourceElement = FindElement(e.OriginalSource, fileDropTargets);
            var files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files?.Length > 0 && sourceElement?.DataContext is IFileDragDropAware fileDragDropAware)
            {
                fileDragDropAware.OnFilesDrop(files.ToList());
            }
        }

        private FrameworkElement FindElement(object originalSource, List<Type> relevantTypes)
        {
            var source = (DependencyObject)originalSource;
            if (relevantTypes != null)
            {
                foreach (var type in relevantTypes)
                {
                    if (Utils.FindParentOfType(source, type) is FrameworkElement sourceParent)
                    {
                        return sourceParent;
                    }
                }
            }
            return frameworkElement;
        }
    }
}