using JToolbox.Core.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class FileSystemHelperTests
    {
        [TestMethod]
        public void FileNameFitsMask_AllFilesNamesShouldMatchMask()
        {
            var mask = "*.txt";
            var fileNames = new List<string>
            {
                "asd.txt",
                "qwe.asd.txt",
            };

            Assert.IsTrue(fileNames.All(x => FileSystemHelper.FileNameFitsMask(x, mask)));
        }

        [TestMethod]
        public void FileNameFitsMask_AllFilesNamesShouldMatchMasks()
        {
            var masks = "*.txt;*.bmp";
            var fileNames = new List<string>
            {
                "asd.txt",
                "asd.bmp",
            };

            Assert.IsTrue(fileNames.All(x => FileSystemHelper.FileNameFitsMasks(x, masks)));
        }

        [TestMethod]
        public void FileNameFitsMask_AllFilesNamesShouldNotMatchMask()
        {
            var mask = "*.txt";
            var fileNames = new List<string>
            {
                "asd.axt",
                ".bmp",
            };

            Assert.IsTrue(fileNames.All(x => !FileSystemHelper.FileNameFitsMask(x, mask)));
        }
    }
}