﻿using JToolbox.Core.Helpers;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace AppZipper
{
    public class Zipper
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
            var packerData = new ZipperData(config, exeFile);

            if (packerData.OutputFilesAndFolders.Any())
            {
                RemovePreviousPackages(packerData, config);
                ArchiveHelper.Pack(packerData.OutputFilePath, packerData.OutputFilesAndFolders);
                PrintSummary(packerData);
            }
            else
            {
                Console.WriteLine("Nothing to pack");
            }
        }

        private void PrintSummary(ZipperData packerData)
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

        private void RemovePreviousPackages(ZipperData packerData, Config config)
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