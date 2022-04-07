namespace AppUploader
{
    public class UploadData : ConnectionData
    {
        public string FilePath { get; set; }
        public string TargetPath { get; set; }

        public void Set(ConnectionData connectionData)
        {
            Hostname = string.IsNullOrEmpty(Hostname) ? connectionData.Hostname : string.Empty;
            Password = string.IsNullOrEmpty(Password) ? connectionData.Password : string.Empty;
            Port = Port == 0 ? connectionData.Port : 0;
            Username = string.IsNullOrEmpty(Username) ? connectionData.Username : string.Empty;
        }
    }
}