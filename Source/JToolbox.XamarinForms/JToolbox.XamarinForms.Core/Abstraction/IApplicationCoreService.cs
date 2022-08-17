using System.Collections.Generic;

namespace JToolbox.XamarinForms.Core.Abstraction
{
    public interface IApplicationCoreService
    {
        string DeviceId { get; }

        string DownloadsFolder { get; }

        string InternalFolder { get; }

        string PrivateExternalFolder { get; }

        string PublicExternalFolder { get; }

        void FilesScan(List<string> files);

        void Kill();
    }
}