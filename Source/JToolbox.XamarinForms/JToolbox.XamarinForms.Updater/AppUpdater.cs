using JToolbox.Misc.Serializers;
using JToolbox.XamarinForms.Core.Abstraction;
using Plugin.XF.AppInstallHelper;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace JToolbox.XamarinForms.Updater
{
    public class AppUpdater
    {
        private readonly IApplicationCoreService applicationCoreService;

        public AppUpdater(IApplicationCoreService applicationCoreService)
        {
            this.applicationCoreService = applicationCoreService;
        }

        public async Task Update(UpdaterSettings settings, Func<Version, Version, Task> onBeforeUpdate)
        {
            using (WebClient client = new WebClient())
            {
                if (settings.UseCredentials)
                {
                    client.Credentials = new NetworkCredential(settings.UserName, settings.Password);
                }

                var data = await Task.Run(() => client.DownloadData(settings.FullUrl));
                var updateFile = new SerializerJson().Deserialize<UpdateFile>(data);

                var newVersion = Version.Parse(updateFile.Version);
                if (newVersion > AppInfo.Version)
                {
                    if (onBeforeUpdate != null)
                    {
                        await onBeforeUpdate(AppInfo.Version, newVersion);
                    }

                    var apkFilePath = Path.Combine(settings.Url, updateFile.Path);
                    var downloadPath = Path.Combine(applicationCoreService.DownloadsFolder, updateFile.Path);
                    await Task.Run(() => client.DownloadFile(apkFilePath, downloadPath));

                    await InstallationHelper.InstallApp(downloadPath, InstallMode.OutOfAppStore);
                }
            }
        }
    }
}