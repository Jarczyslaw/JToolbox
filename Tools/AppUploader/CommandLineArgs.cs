namespace AppUploader
{
    public class CommandLineArgs
    {
        public string FilePath { get; set; }
        public string Hostname { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public string TargetPath { get; set; }
        public string Username { get; set; }
    }
}