using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;

namespace AppPacker
{
    public class Packer
    {
        public void Run(Config config)
        {
            var files = Directory.EnumerateFiles(config.AssemblyFolderPath, "*.exe")
                .ToList();

            if (files.Count == 0)
            {
                Console.WriteLine("No exe file in current build output directory");
                return;
            }

            if (files.Count > 1)
            {
                Console.WriteLine("There are more than one exe file in current build output directory");
                return;
            }

            var exeFile = files.First();
            var packerData = new PackerData(config, exeFile);

            if (packerData.OutputFilesAndFolders.Any())
            {
                RemovePreviousPackages(packerData, config);
                Pack(packerData.OutputFilePath, packerData.OutputFilesAndFolders);
                PrintSummary(packerData);
            }
            else
            {
                Console.WriteLine("Nothing to pack");
            }
        }

        private void Pack(string targetFilePath, List<string> filePaths)
        {
            using (var archive = ZipFile.Open(targetFilePath, ZipArchiveMode.Create))
            {
                foreach (var filePath in filePaths)
                {
                    archive.CreateEntryFromAny(filePath);
                }
            }
        }

        private void PrintSummary(PackerData packerData)
        {
            Console.WriteLine($"{packerData.OutputFileName} created successfully");
            Console.WriteLine("Package content:");

            var sb = new StringBuilder();
            var counter = 1;
            foreach (var path in packerData.OutputFilesAndFolders.OrderBy(x => x))
            {
                var directory = File.GetAttributes(path).HasFlag(FileAttributes.Directory) ? "(Dir)" : string.Empty;
                var name = Path.GetFileName(path);
                sb.AppendLine($"{counter}. {name} {directory}");
                counter++;
            }
            Console.Write(sb.ToString());
        }

        private void RemovePreviousPackages(PackerData packerData, Config config)
        {
            if (File.Exists(packerData.OutputFilePath))
            {
                File.Delete(packerData.OutputFilePath);
            }

            if (config.RemovePreviousPackages)
            {
                var files = Directory.GetFiles(packerData.OutputFolderPath, $"*{packerData.FileName}*.zip");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}