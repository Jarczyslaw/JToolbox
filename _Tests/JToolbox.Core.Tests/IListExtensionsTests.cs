using JToolbox.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class IListExtensionsTests
    {
        private List<string> GetList()
        {
            return new List<string> { "1", "2", "3", "4", "5" };
        }

        [TestMethod]
        public void ShiftLeft1()
        {
            var original = GetList();
            original.ShiftLeft(new List<string> { "1", "2" });
            CollectionAssert.AreEqual(GetList(), original);
        }

        [TestMethod]
        public void ShiftLeft2()
        {
            var original = GetList();
            original.ShiftLeft(new List<string> { "2", "3" });
            CollectionAssert.AreEqual(new List<string> { "2", "3", "1", "4", "5" }, original);
        }

        [TestMethod]
        public void ShiftLeft3()
        {
            var original = GetList();
            original.ShiftLeft(new List<string> { "2", "4" });
            CollectionAssert.AreEqual(new List<string> { "2", "1", "4", "3", "5" }, original);
        }

        [TestMethod]
        public void ShiftLeft4()
        {
            var original = GetList();
            original.ShiftLeft(new List<string> { "1", "3" });
            CollectionAssert.AreEqual(new List<string> { "1", "3", "2", "4", "5" }, original);
        }

        [TestMethod]
        public void ShiftRight1()
        {
            var original = GetList();
            original.ShiftRight(new List<string> { "4", "5" });
            CollectionAssert.AreEqual(GetList(), original);
        }

        [TestMethod]
        public void ShiftRight2()
        {
            var original = GetList();
            original.ShiftRight(new List<string> { "2", "3" });
            CollectionAssert.AreEqual(new List<string> { "1", "4", "2", "3", "5" }, original);
        }

        [TestMethod]
        public void ShiftRight3()
        {
            var original = GetList();
            original.ShiftRight(new List<string> { "2", "4" });
            CollectionAssert.AreEqual(new List<string> { "1", "3", "2", "5", "4" }, original);
        }

        [TestMethod]
        public void ShiftRight4()
        {
            var original = GetList();
            original.ShiftRight(new List<string> { "3", "5" });
            CollectionAssert.AreEqual(new List<string> { "1", "2", "4", "3", "5" }, original);
        }

        [TestMethod]
        public void SetAsFirstFast1()
        {
            var original = GetList();
            original.SetAsFirstFast(new List<string> { "1", "3", "5" });
            CollectionAssert.AreEqual(new List<string> { "1", "3", "5", "2", "4" }, original);
        }

        [TestMethod]
        public void SetAsLastFast1()
        {
            var original = GetList();
            original.SetAsLastFast(new List<string> { "1", "3", "5" });
            CollectionAssert.AreEqual(new List<string> { "2", "4", "1", "3", "5" }, original);
        }

        [TestMethod]
        public void SetAsFirst1()
        {
            var original = GetList();
            original.SetAsFirst(new List<string> { "1", "3", "5" });
            CollectionAssert.AreEqual(new List<string> { "1", "3", "5", "2", "4" }, original);
        }

        [TestMethod]
        public void SetAsLast1()
        {
            var original = GetList();
            original.SetAsLast(new List<string> { "1", "3", "5" });
            CollectionAssert.AreEqual(new List<string> { "2", "4", "1", "3", "5" }, original);
        }
    }
}