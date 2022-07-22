using Acr.UserDialogs;
using JToolbox.XamarinForms.Dialogs.Resources;
using System;

namespace JToolbox.XamarinForms.Dialogs
{
    public class BusyIndicator : IDisposable
    {
        public BusyIndicator(string message)
        {
            message = message ?? Translations.PleaseWait;
            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }

        public void Dispose()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}