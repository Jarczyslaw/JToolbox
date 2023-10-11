using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace JToolbox.Core.Helpers
{
    public static class FileSystemHelper
    {
        private static readonly Random random = new Random();

        public static string ChangeFileExtension(string filePath, string extension, bool preserveOriginalFile = false)
        {
            string changed = Path.ChangeExtension(filePath, extension);
            File.Copy(filePath, changed);

            if (!preserveOriginalFile) { File.Delete(filePath); }
            return changed;
        }

        public static string ChangeFileName(string filePath, string newFileName)
        {
            var path = Path.GetDirectoryName(filePath);
            var extension = Path.GetExtension(filePath);

            return Path.Combine(path, newFileName + extension);
        }

        public static List<string> ChangeFilesExtension(string path, string extension, string pattern = "*.*", bool preserveOriginalFile = false)
        {
            var directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles(pattern);

            foreach (FileInfo file in files)
            {
                ChangeFileExtension(file.FullName, extension, preserveOriginalFile);
            }

            return files.Select(x => x.FullName)
                .ToList();
        }

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

        public static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
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

        public static void DeleteDirectoryWithContent(string folderPath)
        {
            var directoryInfo = new DirectoryInfo(folderPath);

            if (!directoryInfo.Exists) { return; }

            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo directory in directoryInfo.GetDirectories())
            {
                directory.Delete(true);
            }
        }

        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static void DeleteFolder(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
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

        public static bool IsDirectory(string path)
        {
            return File.GetAttributes(path).HasFlag(FileAttributes.Directory);
        }

        public static bool IsFile(string path)
        {
            return !IsDirectory(path);
        }

        public static void IterateFilesAndFolders(string path, Action<FileInfo> fileCallback, Func<DirectoryInfo, bool> directoryCallback)
        {
            if (IsFile(path))
            {
                fileCallback(new FileInfo(path));
            }
            else if (IsDirectory(path))
            {
                var directoryInfo = new DirectoryInfo(path);
                var @continue = directoryCallback(directoryInfo);
                if (@continue)
                {
                    IterateFolders(directoryInfo, fileCallback, directoryCallback);
                }
            }
        }

        public static string RenameFile(string filePath, string newFileName)
        {
            var path = Path.GetDirectoryName(filePath);
            var newFilePath = Path.Combine(path, newFileName);
            File.Move(filePath, newFilePath);
            return newFilePath;
        }

        private static void IterateFolders(DirectoryInfo directory, Action<FileInfo> fileCallback, Func<DirectoryInfo, bool> directoryCallback)
        {
            foreach (var file in directory.EnumerateFiles())
            {
                fileCallback(file);
            }

            foreach (var dir in directory.EnumerateDirectories())
            {
                var @continue = directoryCallback(dir);
                if (@continue)
                {
                    IterateFolders(dir, fileCallback, directoryCallback);
                }
            }
        }
    }
}