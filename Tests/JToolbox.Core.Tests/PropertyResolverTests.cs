using JToolbox.Core.Exceptions;
using JToolbox.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class PropertyResolverTests
    {
        private ExampleClass exampleObject;

        [TestInitialize]
        public void Initialize()
        {
            exampleObject = new ExampleClass
            {
                Nested = new ExampleNestedClass
                {
                    Value = "A",
                    Id = 1,
                    Dictionary = new Dictionary<int, string> { [1] = "A" },
                    List = new List<int> { 1 }
                },
                List = new List<ExampleNestedClass>
                {
                    new ExampleNestedClass
                    {
                        Id = 2,
                        Value = "B",
                        Dictionary = new Dictionary<int, string> { [2] = "B" },
                        List = new List<int> { 2 }
                    }
                },
                Dictionary = new Dictionary<int, ExampleNestedClass>
                {
                    [3] = new ExampleNestedClass
                    {
                        Id = 3,
                        Value = "C",
                        Dictionary = new Dictionary<int, string> { [3] = "C" },
                        List = new List<int> { 3 }
                    }
                }
            };
        }

        [TestMethod]
        public void Resolve_InvalidIndexOrKeyInCollections_ThrowsException()
        {
            Assert.ThrowsException<CanNotAccessCollectionItemException>(() => PropertyResolver.Resolve(exampleObject, "List[10]"));
            Assert.ThrowsException<CanNotAccessCollectionItemException>(() => PropertyResolver.Resolve(exampleObject, "Dictionary[10]"));
        }

        [TestMethod]
        public void Resolve_InvalidPropertyName_ThrowsException()
        {
            Assert.ThrowsException<PropertyNotFoundException>(() => PropertyResolver.Resolve(exampleObject, "asd"));
        }

        [TestMethod]
        public void Resolve_NullDuringResolving_ThrowsException()
        {
            Assert.ThrowsException<CanNotResolvePathException>(() => PropertyResolver.Resolve(exampleObject, "NestedNull.Id"));
        }

        [TestMethod]
        public void Resolve_ValidPaths_ReturnValidValues()
        {
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "Nested.Id"), 1);
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "Nested.Dictionary[1]"), "A");
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "Nested.List[0]"), 1);
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "List[0].Value"), "B");
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "List[0].Dictionary[2]"), "B");
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "Dictionary[3].Value"), "C");
            Assert.AreEqual(PropertyResolver.Resolve(exampleObject, "Dictionary[3].Dictionary[3]"), "C");
        }

        private class ExampleClass
        {
            public Dictionary<int, ExampleNestedClass> Dictionary { get; set; }

            public List<ExampleNestedClass> List { get; set; }

            public ExampleNestedClass Nested { get; set; }

            public ExampleNestedClass NestedNull { get; set; }
        }

        private class ExampleNestedClass
        {
            public Dictionary<int, string> Dictionary { get; set; }

            public int Id { get; set; }

            public List<int> List { get; set; }

            public string Value { get; set; }
        }
    }
}