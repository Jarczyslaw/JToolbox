using JToolbox.Misc.Configuration;
using System;
using System.IO;

namespace AppUploader
{
    public class Configuration : BaseConfiguration
    {
        public string FilePath => GetValue("FilePath");
        public string TargetPath => GetValue("TargetPath");
        protected override string GetConfigurationFilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.json");
    }
}