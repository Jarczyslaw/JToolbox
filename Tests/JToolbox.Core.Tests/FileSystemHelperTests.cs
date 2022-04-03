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

            Assert.IsTrue(fileNames.All(x => FileSystemHelper.FileNameMatchesMask(x, mask)));
        }

        [TestMethod]
        public void FileNameFitsMask_AllFilesNamesShouldMatchMasks()
        {
            var masks = "*.txt;*.bmp;test.*,.file";
            var fileNames = new List<string>
            {
                "asd.txt",
                "asd.bmp",
                "test.mp3",
                ".file"
            };

            Assert.IsTrue(fileNames.All(x => FileSystemHelper.FileNameMatchesMasks(x, masks)));
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

            Assert.IsTrue(fileNames.All(x => !FileSystemHelper.FileNameMatchesMask(x, mask)));
        }
    }
}