using JToolbox.Desktop.Dialogs;
using System;
using System.Windows;

namespace AppUploader
{
    public partial class App : Application
    {
        private readonly DialogsService dialogs = new DialogsService();

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                var uploadData = GetUploadData(e);
                if (uploadData == null) { return; }

                var mainWindow = new MainWindow
                {
                    DataContext = new MainViewModel(dialogs, uploadData)
                };
                mainWindow.Show();
            }
            catch (Exception exc)
            {
                dialogs.ShowException(exc);
            }
        }

        private UploadData GetUploadData(StartupEventArgs e)
        {
            var commandLineParser = new CommandLineParser();
            var parseResult = commandLineParser.Parse(e.Args);
            if (parseResult.IsError)
            {
                dialogs.ShowError(parseResult.Error.Content);
                return null;
            }

            var registryTools = new RegistryTool();
            var connectionData = registryTools.Load();

            var uploadData = parseResult.Value;
            uploadData.Set(connectionData);
            return uploadData;
        }
    }
}