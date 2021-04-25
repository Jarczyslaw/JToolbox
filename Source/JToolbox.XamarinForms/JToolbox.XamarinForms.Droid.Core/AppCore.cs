using Android.App;
using Android.Media;
using Android.OS;
using JToolbox.XamarinForms.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using static Android.Provider.Settings;

namespace JToolbox.XamarinForms.Droid.Core
{
    public class AppCore : IAppCore
    {
        private readonly IPaths paths;
        private string deviceId;

        public AppCore(IPaths paths)
        {
            this.paths = paths;
        }

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
                        throw new Exception("Can not get a deviceId");
                    }
                }
                return deviceId;
            }
        }

        public string LogPath => Path.Combine(paths.PublicExternalFolder, AppInfo.Name);

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