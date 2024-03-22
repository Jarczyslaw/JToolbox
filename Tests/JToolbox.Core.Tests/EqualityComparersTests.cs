using JToolbox.Core.EqualityComparers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class EqualityComparersTests
    {
        [TestMethod]
        public void Equals_OneProperty_ReturnsValidEqualityValues()
        {
            var comparer = new GenericEqualityComparer<Item, int>(x => x.Id);

            Assert.AreEqual(new Item
            {
                Id = 1
            },
            new Item
            {
                Id = 1
            }, comparer);

            Assert.AreNotEqual(new Item
            {
                Id = 1
            },
            new Item
            {
                Id = 2
            }, comparer);

            var item = new Item();
            Assert.AreEqual(item, item, comparer);
            Assert.AreEqual(null, null, comparer);
        }

        [TestMethod]
        public void Equals_TwoProperties_ReturnsValidEqualityValues()
        {
            var comparer = new GenericEqualityComparer<Item, int, string>(x => x.Id, x => x.Name);

            Assert.AreEqual(new Item
            {
                Id = 1,
                Name = "name"
            },
            new Item
            {
                Id = 1,
                Name = "name"
            }, comparer);

            Assert.AreNotEqual(new Item
            {
                Id = 1,
                Name = "name"
            },
            new Item
            {
                Id = 1
            }, comparer);
        }

        private class Item
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}