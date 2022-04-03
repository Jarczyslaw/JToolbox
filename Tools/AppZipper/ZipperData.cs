using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AppZipper
{
    public class ZipperData
    {
        private readonly Config config;

        public ZipperData(Config config, string filePath)
        {
            this.config = config;

            FilePath = filePath;
            FileFolderPath = Path.GetDirectoryName(filePath);
            FileName = Path.GetFileNameWithoutExtension(filePath);
            FileVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
            OutputFileName = GetOutputFile();
            OutputFolderPath = Directory.GetParent(FileFolderPath).FullName;
            OutputFilePath = Path.Combine(OutputFolderPath, OutputFileName);

            GetFilesAndFolders();
        }

        public string FileFolderPath { get; }

        public string FileName { get; }

        public string FilePath { get; }

        public string FileVersion { get; }

        public string OutputFileName { get; }

        public string OutputFilePath { get; }

        public List<string> OutputFilesAndFolders { get; } = new List<string>();

        public string OutputFolderPath { get; }

        private void GetFilesAndFolders()
        {
            var files = Directory.GetFiles(FileFolderPath);
            var folders = Directory.GetDirectories(FileFolderPath);

            foreach (var file in files)
            {
                var fileName = Path.GetFileName(file);
                var fileExtension = Path.GetExtension(file);

                if (config.FilesWhiteListParsed.Contains(fileName))
                {
                    OutputFilesAndFolders.Add(file);
                }
                else
                {
                    if (!config.IgnoredFilesExtensionsParsed.Any(x => fileName.EndsWith(x)))
                    {
                        OutputFilesAndFolders.Add(file);
                    }
                }
            }
            OutputFilesAndFolders.AddRange(folders);
        }

        private string GetOutputFile()
        {
            var pattern = config.OutputFileNamePattern;
            var now = DateTime.Now;
            var values = new Dictionary<string, string>
            {
                ["{assemblyName}"] = FileName,
                ["{version}"] = FileVersion,
                ["{date}"] = now.ToString("yyyy.MM.dd_HH.mm.ss"),
                ["{year}"] = now.Year.ToString(),
                ["{month}"] = now.Month.ToString(),
                ["{day}"] = now.Day.ToString(),
                ["{hour}"] = now.Hour.ToString(),
                ["{minute}"] = now.Minute.ToString(),
                ["{second}"] = now.Second.ToString(),
            };

            foreach (var pair in values)
            {
                pattern = pattern.Replace(pair.Key, pair.Value);
            }

            var extension = ".zip";
            if (!pattern.EndsWith(extension))
            {
                pattern += extension;
            }

            return pattern;
        }
    }
}