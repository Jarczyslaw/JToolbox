using AutoUpdaterDotNET;
using JToolbox.Misc.Serializers;
using System.IO;

namespace AppUploader
{
    public class UploadData : ConnectionData
    {
        public string FilePath { get; set; }
        public string TargetFilePath => Path.Combine(TargetPath, Path.GetFileName(FilePath));
        public string TargetPath { get; set; }

        public string CreateUpdaterFile()
        {
            var fileName = Path.GetFileName(FilePath);
            var updaterFilePath = Path.Combine(Path.GetTempPath(), fileName);

            var updateInfo = new UpdateInfoEventArgs
            {
                DownloadURL = fileName,
                CurrentVersion = GetVersionFromFilename(fileName),
                Mandatory = new Mandatory
                {
                    Value = true,
                    UpdateMode = Mode.Forced
                }
            };

            var serializer = new SerializerXml();
            serializer.ToFile(updateInfo, updaterFilePath);
            return updaterFilePath;
        }

        public void Set(ConnectionData connectionData)
        {
            Hostname = string.IsNullOrEmpty(Hostname) ? connectionData.Hostname : string.Empty;
            Password = string.IsNullOrEmpty(Password) ? connectionData.Password : string.Empty;
            Port = Port == 0 ? connectionData.Port : 0;
            Username = string.IsNullOrEmpty(Username) ? connectionData.Username : string.Empty;
        }

        private string GetVersionFromFilename(string fileName)
        {
            return fileName.Split('_')[1];
        }
    }
}