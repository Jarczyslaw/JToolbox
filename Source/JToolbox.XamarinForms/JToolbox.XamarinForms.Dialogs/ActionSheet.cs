using Acr.UserDialogs;
using System;
using System.Collections.Generic;

namespace JToolbox.XamarinForms.Dialogs
{
    public class ActionSheet<T> : ActionSheetConfig
    {
        public ActionSheet()
        {
            Options = new List<ActionSheetOption>();
        }

        public Action<T> ActionSheetSelected { get; set; }

        public void AddCancel(string title)
        {
            Cancel = new ActionSheetOption(title, () => ActionSheetSelected(default(T)));
        }

        public void AddOption(string title, T value)
        {
            Options.Add(new ActionSheetOption(title, () => ActionSheetSelected(value)));
        }
    }
}