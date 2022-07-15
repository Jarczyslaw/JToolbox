using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace JToolbox.Core.Helpers
{
    public static class ArchiveHelper
    {
        public static void CreateEntryFromAny(this ZipArchive archive, string sourceName, string entryName = "")
        {
            var fileName = Path.GetFileName(sourceName);
            if (File.GetAttributes(sourceName).HasFlag(FileAttributes.Directory))
            {
                archive.CreateEntryFromDirectory(sourceName, Path.Combine(entryName, fileName));
            }
            else
            {
                archive.CreateEntryFromFile(sourceName, Path.Combine(entryName, fileName), CompressionLevel.Optimal);
            }
        }

        public static void CreateEntryFromDirectory(this ZipArchive archive, string sourceDirName, string entryName = "")
        {
            var files = Directory.GetFiles(sourceDirName)
                .Concat(Directory.GetDirectories(sourceDirName))
                .ToArray();

            foreach (var file in files)
            {
                archive.CreateEntryFromAny(file, entryName);
            }
        }

        public static void Pack(string targetFilePath, List<string> filePaths)
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