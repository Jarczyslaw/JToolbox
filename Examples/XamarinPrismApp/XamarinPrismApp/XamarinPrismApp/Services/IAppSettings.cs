using JToolbox.XamarinForms.Settings;

namespace XamarinPrismApp.Services
{
    public interface IAppSettings : IApplicationSettings
    {
        string Value { get; set; }
    }
}