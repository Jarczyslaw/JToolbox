using Acr.UserDialogs;
using System;

namespace JToolbox.XamarinForms.Dialogs
{
    public class BusyIndicator : IDisposable
    {
        public BusyIndicator(string message)
        {
            UserDialogs.Instance.ShowLoading(message, MaskType.Gradient);
        }

        public void Dispose()
        {
            UserDialogs.Instance.HideLoading();
        }
    }
}