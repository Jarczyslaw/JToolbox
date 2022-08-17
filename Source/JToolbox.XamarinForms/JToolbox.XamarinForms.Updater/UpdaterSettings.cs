using System.IO;

namespace JToolbox.XamarinForms.Updater
{
    public class UpdaterSettings
    {
        public string FullUrl => Path.Combine(Url, "update.json");

        public string Password { get; set; }

        public string Url { get; set; }

        public bool UseCredentials => !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(UserName);

        public string UserName { get; set; }
    }
}