using System.IO;

namespace AppUploader
{
    public class UploadData : ConnectionData
    {
        private readonly string updaterFileName = "update.xml";

        public string FilePath { get; set; }
        public string TargetFilePath => Path.Combine(TargetPath, Path.GetFileName(FilePath));
        public string TargetPath { get; set; }
        public string UpdaterFilePath => Path.Combine(TargetPath, updaterFileName);

        public string CreateUpdaterFile()
        {
            var fileName = Path.GetFileName(FilePath);
            var updaterFilePath = Path.Combine(Path.GetTempPath(), updaterFileName);

            var updaterFileContent = new UpdaterFile
            {
                Url = fileName,
                Version = GetVersionFromFilename(fileName),
                Mandatory = "true"
            };

            updaterFileContent.Serialize(updaterFilePath);
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
            fileName = Path.GetFileNameWithoutExtension(fileName);
            return fileName.Split('_')[1];
        }
    }
}