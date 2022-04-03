using JToolbox.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppZipper
{
    public class Config
    {
        private const string assemblyNameTag = "{assemblyName}";
        private const string splitCharacter = ";";

        public string AssemblyFolderPath { get; set; }

        public string FilesWhiteList { get; set; }

        [JsonIgnore]
        public IEnumerable<string> FilesWhiteListParsed => SplitString(FilesWhiteList);

        public string IgnoredFilesExtensions { get; set; } = "dll.config;pdb;xml";

        [JsonIgnore]
        public IEnumerable<string> IgnoredFilesExtensionsParsed => SplitString(IgnoredFilesExtensions);

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

            if (!string.IsNullOrEmpty(FilesWhiteList))
            {
                if (FilesWhiteListParsed.Any(x => !x.IsValidFileName()))
                {
                    Console.WriteLine("Not all values in white list are valid file names");
                    return false;
                }
            }

            if (!string.IsNullOrEmpty(IgnoredFilesExtensions))
            {
                if (IgnoredFilesExtensionsParsed.Any(x => !x.IsValidFileName()))
                {
                    Console.WriteLine("Not all values in ignored files extensions are valid");
                    return false;
                }
            }

            return true;
        }

        private IEnumerable<string> SplitString(string @string)
        {
            if (string.IsNullOrEmpty(@string))
            {
                return new List<string>();
            }

            return @string.Split(new string[] { splitCharacter }, StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim());
        }
    }
}