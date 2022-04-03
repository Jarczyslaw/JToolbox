using JToolbox.Desktop.Dialogs;
using System;
using System.Windows;

namespace AppUploader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var dialogs = new DialogsService();

            try
            {
                var commandLineParser = new CommandLineParser();
                var parseResult = commandLineParser.Parse(e.Args);
                if (parseResult.IsError)
                {
                    dialogs.ShowError(parseResult.Error.Content);
                    return;
                }

                var mainViewModel = new MainViewModel(dialogs);

                var mainWindow = new MainWindow
                {
                    DataContext = mainViewModel
                };
                mainWindow.Show();
            }
            catch (Exception exc)
            {
                dialogs.ShowException(exc);
            }
        }
    }
}