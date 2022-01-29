namespace JToolbox.Misc.AppsUpdater
{
    public class UpdaterSettings
    {
        public string Password { get; set; }
        public bool UseAuthentication => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
        public string UserName { get; set; }
        public bool WaitForUpdateCallback { get; set; }
        public string XmlPath { get; set; }
    }
}