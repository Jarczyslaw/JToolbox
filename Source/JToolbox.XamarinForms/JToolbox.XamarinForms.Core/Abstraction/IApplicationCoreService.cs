namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IApplicationCoreService
    {
        string DeviceId { get; }
        string InternalFolder { get; }
        string LogPath { get; }
        string PrivateExternalFolder { get; }

        void Kill();
    }
}