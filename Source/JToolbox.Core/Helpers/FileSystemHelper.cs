using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JToolbox.Core.Helpers
{
    public static class FileSystemHelper
    {
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

        public static bool FileNameFitsMask(string fileName, string fileMask)
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

        public static bool FileNameFitsMasks(string fileName, params string[] fileMasks)
        {
            if (fileMasks?.Length > 0)
            {
                return fileMasks.Any(x => FileNameFitsMask(fileName, x));
            }
            return false;
        }

        public static bool FileNameFitsMasks(string fileName, string fileMasks)
        {
            var masks = fileMasks.Split(new string[] { "\r\n", "\n", ",", "|", " ", ";" },
                StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim());

            return FileNameFitsMasks(fileName, masks.ToArray());
        }
    }
}