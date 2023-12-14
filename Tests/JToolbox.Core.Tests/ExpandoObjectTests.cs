using JToolbox.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Dynamic;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class ExpandoObjectTests
    {
        [TestMethod]
        public void ExpandoTest()
        {
            var expando = new ExpandoObject();

            expando.Set("id", 2);
            expando.Set("name", "Name");
            expando.Set("value");

            expando.Set("value", true);

            Assert.AreEqual(expando.Get("id"), 2);
            Assert.AreEqual(expando.Get("name"), "Name");
            Assert.AreEqual(expando.Get("value"), true);
            Assert.ThrowsException<ArgumentException>(() => expando.Get("invalid"));
        }
    }
}