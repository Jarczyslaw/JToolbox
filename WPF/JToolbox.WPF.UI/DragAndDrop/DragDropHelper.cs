using JToolbox.WPF.Core.Awareness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public delegate void OnDrag(UiDragDropArgs args);

    public delegate void OnDrop(UiDragDropArgs args);

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

        public List<Type> DragDropSources => dragDropPairs.Select(s => s.SourceType).ToList();

        public List<Type> DragDropTargets => dragDropPairs.Select(s => s.TargetType).ToList();

        private void CallOnDragChain(UiDragDropArgs args)
        {
            args.Handled = false;
            OnDrag?.Invoke(args);
            if (args.Handled)
            {
                return;
            }

            if (args.Source is IDragDropAware sourceAware)
            {
                sourceAware.OnDrag(args);
                if (args.Handled)
                {
                    return;
                }
            }

            if (args.SourceElement != frameworkElement && frameworkElement.DataContext is IDragDropAware elementAware)
            {
                elementAware.OnDrag(args);
            }
        }

        private void CallOnDropChain(UiDragDropArgs args)
        {
            args.Handled = false;
            OnDrop?.Invoke(args);
            if (args.Handled)
            {
                return;
            }

            if (args.Target is IDragDropAware targetAware)
            {
                targetAware.OnDrop(args);
                if (args.Handled)
                {
                    return;
                }
            }

            if (args.TargetElement != frameworkElement && frameworkElement.DataContext is IDragDropAware elementAware)
            {
                elementAware.OnDrop(args);
            }
        }

        protected override void DragStart(object sender, MouseEventArgs e)
        {
            var source = (DependencyObject)e.OriginalSource;
            if (Utils.FindParentOfTypes(source, DragDropSources) is FrameworkElement sourceParent)
            {
                var args = new UiDragDropArgs
                {
                    SourceElement = sourceParent,
                    Source = sourceParent.DataContext
                };
                CallOnDragChain(args);
                DragDrop.DoDragDrop(source, new DataObject(Key, args), DragDropEffects.Link);
                startPosition = null;
            }
        }

        protected override void DropStart(object sender, DragEventArgs e)
        {
            var args = e.Data.GetData(Key) as UiDragDropArgs;
            var targetTypes = dragDropPairs.Where(d => d.SourceType == args.SourceElement.GetType())
                .Select(s => s.TargetType)
                .ToList();
            var target = (DependencyObject)e.OriginalSource;
            if (Utils.FindParentOfTypes(target, targetTypes) is FrameworkElement targetParent
                && args.SourceElement != targetParent)
            {
                args.TargetElement = targetParent;
                args.Target = targetParent.DataContext;
                CallOnDropChain(args);
            }
        }
    }
}