using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

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
                if (File.Exists(packerData.OutputFilePath))
                {
                    File.Delete(packerData.OutputFilePath);
                }

                Pack(packerData.OutputFilePath, packerData.OutputFilesAndFolders);
                Console.WriteLine($"{packerData.OutputFileName} with {packerData.OutputFilesAndFolders.Count} elements created successfully");
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
    }
}