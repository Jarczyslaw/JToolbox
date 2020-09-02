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

    public class DragDropHelper
    {
        private readonly string key;
        private readonly FrameworkElement element;
        private readonly List<DragDropPair> dragDropPairs;
        private Point? startPosition;

        public event OnDrag OnDrag;

        public event OnDrop OnDrop;

        public DragDropHelper(FrameworkElement element, string key,
            List<DragDropPair> dragDropPairs)
        {
            this.element = element;
            this.key = key;
            this.dragDropPairs = dragDropPairs;

            PinEvents();
        }

        public void PinEvents()
        {
            UnpinEvents();
            element.PreviewMouseLeftButtonDown += PreviewMouseLeftButtonDown;
            element.MouseMove += MouseMove;
            element.DragEnter += DragEnter;
            element.Drop += Drop;
        }

        public void UnpinEvents()
        {
            element.PreviewMouseLeftButtonDown -= PreviewMouseLeftButtonDown;
            element.MouseMove -= MouseMove;
            element.DragEnter -= DragEnter;
            element.Drop -= Drop;
        }

        private void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPosition = e.GetPosition(null);
        }

        protected virtual void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && CheckMinimumDistance(e.GetPosition(null)))
            {
                foreach (var dragDropPair in dragDropPairs)
                {
                    var source = (DependencyObject)e.OriginalSource;
                    if (Utils.FindParentOfType(source, dragDropPair.SourceType) is FrameworkElement sourceParent)
                    {
                        var dragData = new DataObject(key, new DragData
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
        }

        private void DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(key))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        protected virtual void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(key))
            {
                var dragData = e.Data.GetData(key) as DragData;
                foreach (var dragDropPair in dragDropPairs.Where(d => d.SourceType == dragData.SourceType))
                {
                    var target = (DependencyObject)e.OriginalSource;
                    if (Utils.FindParentOfType(target, dragDropPair.TargetType) is FrameworkElement targetParent
                        && dragData.Data != targetParent.DataContext
                        && element.DataContext is IDragDropAware dragAware)
                    {
                        dragAware.OnDragDrop(dragData.Data, targetParent.DataContext);
                        OnDrop?.Invoke(targetParent, dragData);
                        return;
                    }
                }
            }
        }

        private bool CheckMinimumDistance(Point position)
        {
            if (startPosition != null)
            {
                var diff = startPosition.Value - position;
                return Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance;
            }
            return false;
        }

        private class DragData
        {
            public Type SourceType { get; set; }
            public object Data { get; set; }
        }
    }
}