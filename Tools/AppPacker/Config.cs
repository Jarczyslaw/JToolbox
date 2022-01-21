using System;

namespace AppPacker
{
    public class Config
    {
        private const string assemblyNameTag = "{assemblyName}";

        public string AssemblyFolderPath { get; set; }
        public bool IgnoreExeConfigFile { get; set; }
        public bool IgnorePdbFiles { get; set; } = true;
        public bool IgnoreXmlFiles { get; set; } = true;
        public string OutputFileNamePattern { get; set; } = $"{assemblyNameTag}_{{version}}";

        public bool RemovePreviousPackages { get; set; } = true;

        public bool Validate()
        {
            if (!OutputFileNamePattern.Contains(assemblyNameTag))
            {
                Console.WriteLine("Output file name pattern should contain {assemblyName} tag");
                return false;
            }

            if (string.IsNullOrEmpty(AssemblyFolderPath))
            {
                Console.WriteLine("Assembly folder path can not be empty");
                return false;
            }

            return true;
        }
    }
}