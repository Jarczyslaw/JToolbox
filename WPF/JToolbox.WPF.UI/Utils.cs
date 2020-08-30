using System;
using System.Windows;
using System.Windows.Media;

namespace JToolbox.WPF.UI
{
    public static class Utils
    {
        public static T FindParentOfType<T>(DependencyObject current)
            where T : DependencyObject
        {
            return FindParentOfType(current, typeof(T)) as T;
        }

        public static object FindParentOfType(DependencyObject current, Type parentType)
        {
            do
            {
                if (current.GetType() == parentType)
                {
                    return current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
    }
}