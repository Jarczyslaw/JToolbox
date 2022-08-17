using Android.App;
using Android.Media;
using Android.OS;
using JToolbox.XamarinForms.Core.Abstraction;
using System.Collections.Generic;
using System.IO;
using static Android.Provider.Settings;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class ApplicationCoreService : IApplicationCoreService
    {
        private string deviceId;

        public string DeviceId
        {
            get
            {
                if (deviceId == null)
                {
                    try
                    {
                        var context = Application.Context;
                        deviceId = Secure.GetString(context.ContentResolver, Secure.AndroidId);
                    }
                    catch { }

                    if (string.IsNullOrEmpty(deviceId))
                    {
                        throw new System.Exception("Can not get a deviceId");
                    }
                }
                return deviceId;
            }
        }

        public string DownloadsFolder => Path.Combine(PublicExternalFolder, Environment.DirectoryDownloads);

        public string InternalFolder => Application.Context.FilesDir.AbsolutePath;

        public string PrivateExternalFolder => Application.Context.GetExternalFilesDir(null).AbsolutePath;

        public string PublicExternalFolder => Environment.ExternalStorageDirectory.AbsolutePath;

        public void FilesScan(List<string> files)
        {
            MediaScannerConnection.ScanFile(Application.Context, files.ToArray(), null, null);
        }

        public void Kill()
        {
            Process.KillProcess(Process.MyPid());
        }
    }
}