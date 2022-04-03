using JToolbox.Misc.DataProtectionApi;
using Microsoft.Win32;

namespace AppUploader
{
    public class RegistryTool
    {
        private readonly RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64);
        private readonly string entropy = "f1eb1d55-fd17-4a6a-9167-fbebeba359a7";
        private readonly string subKey = "Software\\AppUploader";

        public ConnectionData Load()
        {
            var key = baseKey.OpenSubKey(subKey);
            if (key == null) { return null; }

            return new ConnectionData
            {
                Hostname = (string)key.GetValue(nameof(ConnectionData.Hostname), string.Empty),
                Port = (int)key.GetValue(nameof(ConnectionData.Port), 1),
                Username = Decrypt((string)key.GetValue(nameof(ConnectionData.Username), string.Empty)),
                Password = Decrypt((string)key.GetValue(nameof(ConnectionData.Password), string.Empty)),
            };
        }

        public void Save(ConnectionData data)
        {
            var key = baseKey.CreateSubKey(subKey);
            key.SetValue(nameof(ConnectionData.Hostname), data.Hostname);
            key.SetValue(nameof(ConnectionData.Port), data.Port);
            key.SetValue(nameof(ConnectionData.Username), Encrypt(data.Username));
            key.SetValue(nameof(ConnectionData.Password), Encrypt(data.Password));
        }

        private string Decrypt(string data)
        {
            return DataProtection.Decrypt(data, entropy, false);
        }

        private string Encrypt(string data)
        {
            return DataProtection.Encrypt(data, entropy, false);
        }
    }
}