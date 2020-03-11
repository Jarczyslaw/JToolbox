using JToolbox.XamarinForms.Settings;

namespace XamarinPrismApp.Services
{
    public class AppSettings : ApplicationSettings, IAppSettings
    {
        public string Value
        {
            get => GetString(nameof(Value));
            set => AddString(nameof(Value), value);
        }
    }
}