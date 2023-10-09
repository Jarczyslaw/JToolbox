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
    }
}