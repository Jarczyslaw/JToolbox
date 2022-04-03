using Newtonsoft.Json;
using System;
using System.Linq;

namespace AppZipper
{
    public class Config
    {
        private const string assemblyNameTag = "{assemblyName}";
        private const string splitCharacter = ";";

        public string AssemblyFolderPath { get; set; }

        public string FilesBlackList { get; set; } = "*.dll.config;*.pdb;*.xml";

        [JsonIgnore]
        public string[] FilesBlackListParsed => SplitString(FilesBlackList);

        public string FilesWhiteList { get; set; }

        [JsonIgnore]
        public string[] FilesWhiteListParsed => SplitString(FilesWhiteList);

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

        private string[] SplitString(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return new string[0];
            }

            return @string.Split(new string[] { splitCharacter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
        }
    }
}