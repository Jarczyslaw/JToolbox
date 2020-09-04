using JToolbox.WPF.Core.Awareness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public delegate void OnDrag(FrameworkElement source);

    public delegate void OnDrop(FrameworkElement target, object data);

    public class DragDropHelper : BaseDragDropHelper
    {
        private readonly List<DragDropPair> dragDropPairs;

        public event OnDrag OnDrag;

        public event OnDrop OnDrop;

        public DragDropHelper(FrameworkElement frameworkElement, List<DragDropPair> dragDropPairs)
            : base(frameworkElement)
        {
            this.dragDropPairs = dragDropPairs;
        }

        protected override string Key => nameof(DragDropHelper);

        protected override void DragStart(object sender, MouseEventArgs e)
        {
            foreach (var dragDropPair in dragDropPairs)
            {
                var source = (DependencyObject)e.OriginalSource;
                if (Utils.FindParentOfType(source, dragDropPair.SourceType) is FrameworkElement sourceParent)
                {
                    var dragData = new DataObject(Key, new DragData
                    {
                        SourceType = dragDropPair.SourceType,
                        Data = sourceParent.DataContext
                    });
                    DragDrop.DoDragDrop(source, dragData, DragDropEffects.Link);
                    OnDrag?.Invoke(sourceParent);
                    startPosition = null;
                    return;
                }
            }
        }

        protected override void DropStart(object sender, DragEventArgs e)
        {
            var dragData = e.Data.GetData(Key) as DragData;
            foreach (var dragDropPair in dragDropPairs.Where(d => d.SourceType == dragData.SourceType))
            {
                var target = (DependencyObject)e.OriginalSource;
                if (Utils.FindParentOfType(target, dragDropPair.TargetType) is FrameworkElement targetParent
                    && dragData.Data != targetParent.DataContext
                    && frameworkElement.DataContext is IDragDropAware dragAware)
                {
                    dragAware.OnDragDrop(dragData.Data, targetParent.DataContext);
                    OnDrop?.Invoke(targetParent, dragData);
                    return;
                }
            }
        }

        private class DragData
        {
            public Type SourceType { get; set; }
            public object Data { get; set; }
        }
    }
}