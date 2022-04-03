using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JToolbox.Core.Helpers
{
    public static class FileSystemHelper
    {
        private static readonly Random random = new Random();

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (FileInfo fi in source.GetFiles())
            {
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
            }

            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        public static void CreateEmptyFile(string filePath, long size)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.SetLength(size);
            }
        }

        public static void CreateRandomFile(string filePath, long size)
        {
            var data = new byte[size];
            random.NextBytes(data);
            File.WriteAllBytes(filePath, data);
        }

        public static bool FileNameMatchesMask(string fileName, string fileMask)
        {
            var mask = new Regex(
                '^' +
                fileMask
                    .Replace(".", "[.]")
                    .Replace("*", ".*")
                    .Replace("?", ".")
                + '$',
                RegexOptions.IgnoreCase);
            return mask.IsMatch(fileName);
        }

        public static bool FileNameMatchesMasks(string fileName, params string[] fileMasks)
        {
            if (fileMasks?.Length > 0)
            {
                return fileMasks.Any(x => FileNameMatchesMask(fileName, x));
            }
            return false;
        }

        public static bool FileNameMatchesMasks(string fileName, string fileMasks)
        {
            var masks = fileMasks.Split(new string[] { "\r\n", "\n", ",", "|", " ", ";" },
                StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

            return FileNameMatchesMasks(fileName, masks.ToArray());
        }
    }
}