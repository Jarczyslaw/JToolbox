﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace JToolbox.WinForms.Core.Extensions
{
    public static class ControlExtensions
    {
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

        public static Form FindParentForm(this Control control)
        {
            return FindParentOfType<Form>(control);
        }

        public static T FindParentOfType<T>(this Control control)
        {
            if (control.Parent is T parent)
            {
                return parent;
            }

            if (control.Parent != null)
            {
                return FindParentOfType<T>(control.Parent);
            }
            return default;
        }

        public static void IterateOverControls(this Control parent, Func<Control, bool> action, bool callActionForParent = false)
        {
            if (callActionForParent && !action(parent))
            {
                return;
            }
            foreach (Control control in parent.Controls)
            {
                if (action(control))
                {
                    IterateOverControls(control, action);
                }
            }
        }

        public static void SafeBeginInvoke(this Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.BeginInvoke(action);
            }
            else
            {
                action();
            }
        }

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

        public static T SafeInvoke<T>(this Control control, Func<T> func)
        {
            if (control.InvokeRequired)
            {
                T result = default;
                Action action = () => result = func();
                control.Invoke(action);
                return result;
            }
            else
            {
                return func();
            }
        }
    }
}