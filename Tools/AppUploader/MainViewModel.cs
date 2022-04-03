using JToolbox.WPF.Core.Base;

namespace AppUploader
{
    public class MainViewModel : BaseViewModel
    {
        private string busyContent;
        private string filePath;
        private string hostName;
        private bool isBusy;
        private string password;
        private int port;
        private RelayCommand saveConnectionCommand;
        private RelayCommand selectFileCommand;
        private string targetPath;
        private RelayCommand uploadCommand;
        private string userName;

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

        public string HostName
        {
            get => hostName;
            set => Set(ref hostName, value);
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

        public RelayCommand SelectFileCommand => selectFileCommand ?? (selectFileCommand = new RelayCommand(SelectFile));

        public string TargetPath
        {
            get => targetPath;
            set => Set(ref targetPath, value);
        }

        public RelayCommand UploadCommand => uploadCommand ?? (uploadCommand = new RelayCommand(Upload));

        public string UserName
        {
            get => userName;
            set => Set(ref userName, value);
        }

        private void SaveConnection()
        {
        }

        private void SelectFile()
        {
        }

        private void Upload()
        {
        }
    }
}