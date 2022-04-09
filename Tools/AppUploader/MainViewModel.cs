using JToolbox.Desktop.Dialogs;
using JToolbox.WPF.Core.Base;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppUploader
{
    public class MainViewModel : BaseViewModel, IUploadProgressHandler
    {
        private readonly DialogsService dialogs;
        private string busyContent;
        private string filePath;
        private string hostname;
        private bool isBusy;
        private string password;
        private int port;
        private RelayCommand saveConnectionCommand;
        private string targetPath;
        private RelayCommand uploadCommand;
        private string username;

        public MainViewModel(DialogsService dialogs, UploadData uploadData)
        {
            this.dialogs = dialogs;

            LoadUploadData(uploadData);
            LoadConfiguration();
        }

        public string BusyContent
        {
            get => busyContent;
            set
            {
                Set(ref busyContent, value);
                IsBusy = !string.IsNullOrEmpty(value);
            }
        }

        public string FilePath
        {
            get => filePath;
            set => Set(ref filePath, value);
        }

        public string Hostname
        {
            get => hostname;
            set => Set(ref hostname, value);
        }

        public bool IsBusy
        {
            get => isBusy;
            set => Set(ref isBusy, value);
        }

        public string Password
        {
            get => password;
            set => Set(ref password, value);
        }

        public int Port
        {
            get => port;
            set => Set(ref port, value);
        }

        public RelayCommand SaveConnectionCommand => saveConnectionCommand ?? (saveConnectionCommand = new RelayCommand(SaveConnection));

        public string TargetPath
        {
            get => targetPath;
            set => Set(ref targetPath, value);
        }

        public RelayCommand UploadCommand => uploadCommand ?? (uploadCommand = new RelayCommand(Upload, UploadEnabled));

        public string Username
        {
            get => username;
            set => Set(ref username, value);
        }

        public void OnProgress(string taskName, double progress)
        {
            BusyContent = $"{taskName}: {progress:F0}%";
        }

        private UploadData GetUploadData()
        {
            return new UploadData
            {
                Hostname = Hostname,
                Password = Password,
                Username = Username,
                Port = Port,
                FilePath = FilePath,
                TargetPath = TargetPath,
            };
        }

        private void LoadConfiguration()
        {
            var configuration = new Configuration();
            var filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configuration.FilePath));
            var appFile = SearchForAppFile(filePath);
            if (!string.IsNullOrEmpty(appFile))
            {
                FilePath = appFile;
            }
            TargetPath = configuration.TargetPath;
        }

        private void LoadUploadData(UploadData uploadData)
        {
            FilePath = uploadData.FilePath;
            Hostname = uploadData.Hostname;
            Password = uploadData.Password;
            Port = uploadData.Port;
            TargetPath = uploadData.TargetPath;
            Username = uploadData.Username;
        }

        private async Task Run(Func<Task> action, string message = "Please wait...")
        {
            try
            {
                BusyContent = message;
                await action();
            }
            finally
            {
                BusyContent = null;
            }
        }

        private async void SaveConnection()
        {
            try
            {
                var registryTools = new RegistryTool();
                var connectionData = GetUploadData();
                await Run(() => Task.Run(() => registryTools.Save(connectionData)));
                dialogs.ShowInfo("Connection data saved");
            }
            catch (Exception ex)
            {
                dialogs.ShowException(ex);
            }
        }

        private string SearchForAppFile(string filePath)
        {
            var foundFiles = Directory.EnumerateFiles(filePath, "*.zip");
            if (foundFiles.Count() == 1)
            {
                return foundFiles.First();
            }
            return null;
        }

        private async void Upload()
        {
            try
            {
                var uploadData = GetUploadData();

                var ftpUploader = new FtpUploader();
                await ftpUploader.UploadAppFile(uploadData, this);
                dialogs.ShowInfo("Upload completed");
            }
            catch (Exception ex)
            {
                dialogs.ShowException(ex);
            }
            finally
            {
                BusyContent = null;
            }
        }

        private bool UploadEnabled()
        {
            return !string.IsNullOrEmpty(FilePath)
                && !string.IsNullOrEmpty(Hostname)
                && !string.IsNullOrEmpty(TargetPath);
        }
    }
}