using AutoUpdaterDotNET;
using System;
using System.Threading;

namespace JToolbox.Misc.AppsUpdater
{
    public abstract class Updater
    {
        private readonly ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private bool updatePending;

        public bool? Update(UpdaterSettings settings)
        {
            SetAuthentication(settings);

            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
            AutoUpdater.Start(settings.XmlPath);

            if (settings.WaitForUpdateCallback)
            {
                manualResetEvent.WaitOne();
                return updatePending;
            }
            else
            {
                return null;
            }
        }

        protected virtual bool HandleUpdate(UpdateInfoEventArgs args)
        {
            return true;
        }

        protected virtual void HandleUpdateCheckError(Exception exception)
        { }

        protected virtual void HandleUpdateError(Exception exception)
        { }

        protected abstract void OnApplicationExit();

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args.Error != null)
            {
                HandleUpdateCheckError(args.Error);
                return;
            }

            if (args.IsUpdateAvailable && HandleUpdate(args))
            {
                try
                {
                    if (AutoUpdater.DownloadUpdate(args))
                    {
                        updatePending = true;
                        OnApplicationExit();
                    }
                }
                catch (Exception exception)
                {
                    HandleUpdateError(exception);
                }
            }

            manualResetEvent.Set();
        }

        private void SetAuthentication(UpdaterSettings settings)
        {
            if (settings.UseAuthentication)
            {
                var basicAuthentication = new BasicAuthentication(settings.UserName, settings.Password);
                AutoUpdater.BasicAuthXML =
                    AutoUpdater.BasicAuthDownload =
                    AutoUpdater.BasicAuthChangeLog = basicAuthentication;
            }
        }
    }
}