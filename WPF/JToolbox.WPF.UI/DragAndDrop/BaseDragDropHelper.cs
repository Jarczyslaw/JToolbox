using System;
using System.Windows;
using System.Windows.Input;

namespace JToolbox.WPF.UI.DragAndDrop
{
    public abstract class BaseDragDropHelper
    {
        protected readonly FrameworkElement frameworkElement;
        protected Point? startPosition;

        protected BaseDragDropHelper(FrameworkElement frameworkElement)
        {
            this.frameworkElement = frameworkElement;
            this.frameworkElement.AllowDrop = true;
            PinEvents();
        }

        protected virtual string Key { get; }

        public void PinEvents()
        {
            UnpinEvents();
            frameworkElement.PreviewMouseLeftButtonDown += PreviewMouseLeftButtonDown;
            frameworkElement.MouseMove += MouseMove;
            frameworkElement.DragEnter += DragEnter;
            frameworkElement.Drop += Drop;
        }

        public void UnpinEvents()
        {
            frameworkElement.PreviewMouseLeftButtonDown -= PreviewMouseLeftButtonDown;
            frameworkElement.MouseMove -= MouseMove;
            frameworkElement.DragEnter -= DragEnter;
            frameworkElement.Drop -= Drop;
        }

        private void PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPosition = e.GetPosition(null);
        }

        protected virtual void MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && CheckMinimumDistance(e.GetPosition(null)))
            {
                DragStart(sender, e);
            }
        }

        protected abstract void DragStart(object sender, MouseEventArgs e);

        protected virtual void DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(Key))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;
            }
        }

        protected virtual void Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(Key))
            {
                DropStart(sender, e);
            }
        }

        protected abstract void DropStart(object sender, DragEventArgs e);

        protected bool CheckMinimumDistance(Point position)
        {
            if (startPosition != null)
            {
                var diff = startPosition.Value - position;
                return Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance;
            }
            return false;
        }
    }
}