using JToolbox.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void IndexesOf_EmptyArguments_ShouldReturnEmptyArray()
        {
            var @string = "";
            var toFind = "";
            Assert.IsTrue(@string.IndexesOf(toFind).Count == 0);

            @string = null;
            toFind = null;
            Assert.IsTrue(@string.IndexesOf(toFind).Count == 0);
        }

        [TestMethod]
        public void IndexesOf_ValidArguments_ShouldReturnValidListOfIndexes()
        {
            CollectionAssert.AreEqual("ABC".IndexesOf("B"), new List<int> { 1 });
            CollectionAssert.AreEqual("BAB".IndexesOf("B"), new List<int> { 0, 2 });
            CollectionAssert.AreEqual("BBB".IndexesOf("B"), new List<int> { 0, 1, 2 });
            CollectionAssert.AreEqual("BBB".IndexesOf("C"), new List<int>());
            CollectionAssert.AreEqual("testtesttest".IndexesOf("tt"), new List<int>() { 3, 7 });
            CollectionAssert.AreEqual("testtesttest".IndexesOf("st"), new List<int>() { 2, 6, 10 });
        }

        [TestMethod]
        public void SmartReplaceMany_EmptyArguments_ShouldReturnInputString()
        {
            var @string = "test";
            Assert.AreEqual(@string, @string.SmartReplaceMany(null));
        }

        [TestMethod]
        public void SmartReplaceMany_ValidArguments_ShouldReturnValidResult()
        {
            Assert.AreEqual("testtest".SmartReplaceMany(new Dictionary<string, string> { ["te"] = "Te", ["s"] = "sss" }), "TessstTessst");
            Assert.AreEqual("A".SmartReplaceMany(new Dictionary<string, string> { ["A"] = "B" }), "B");

            // In contrast to SmartReplaceMany ReplaceMany can change input string many times
            // which can lead to weird results when toReplace contains specific values in specific order
            Assert.AreEqual("A".ReplaceMany(new Dictionary<string, string> { ["A"] = "B", ["B"] = "C" }), "C");
            Assert.AreEqual("A".SmartReplaceMany(new Dictionary<string, string> { ["A"] = "B", ["B"] = "C" }), "B");

            Assert.AreEqual("TEST".SmartReplaceMany(new Dictionary<string, string> { ["TEST"] = "" }), "");
            Assert.AreEqual("TEST".SmartReplaceMany(new Dictionary<string, string> { ["T"] = "", ["S"] = "" }), "E");
            Assert.AreEqual("TEST".SmartReplaceMany(new Dictionary<string, string> { ["T"] = "XX", ["S"] = "XX" }), "XXEXXXX");
            Assert.AreEqual("TTYYTTYY".SmartReplaceMany(new Dictionary<string, string> { ["T"] = "X", ["TT"] = "YY" }), "XXYYXXYY");

            Assert.AreEqual("Test Example Test".SmartReplaceMany(new Dictionary<string, string> { ["est"] = "2", ["am"] = "AAMMM" }), "T2 ExAAMMMple T2");
            Assert.AreEqual("fileName.jpg".SmartReplaceMany(new Dictionary<string, string> { ["file"] = "name", ["Name"] = "File" }), "nameFile.jpg");
        }
    }
}