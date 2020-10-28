using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Extensions
{
    public static class ControlExtensions
    {
        public static void SafeInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static List<T> FindControlsOfType<T>(this Control @this, bool includeSubclasses)
            where T : Control
        {
            return @this.FindControlsOfType(typeof(T), includeSubclasses)
                .Cast<T>()
                .ToList();
        }

        public static List<Control> FindControlsOfType(this Control @this, Type type, bool includeSubclasses)
        {
            var result = new List<Control>();
            foreach (Control control in @this.Controls)
            {
                var controlType = control.GetType();
                if (controlType == type || (includeSubclasses && controlType.IsSubclassOf(type)))
                {
                    result.Add(control);
                }
                @this.FindControlsOfType(type, includeSubclasses);
            }
            return result;
        }
    }
}