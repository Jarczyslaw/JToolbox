using JToolbox.Core.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JToolbox.Core.Tests.Nodes
{
    [TestClass]
    public class NodesTests
    {
        private readonly NodesTestsDataSource dataSource = new NodesTestsDataSource();

        [TestMethod]
        public void CreateNodesCollectionTest()
        {
            var collection = dataSource.CreateNodesCollection();
            Assert.AreEqual(3, collection.NodesCount);
            Assert.AreEqual(16, collection.AllNodesCount);
        }

        [TestMethod]
        public void CompareTest_CollectionsAreTheSame()
        {
            var collection1 = dataSource.CreateNodesCollection();
            var collection2 = dataSource.CreateNodesCollection();
            Assert.IsTrue(collection1.CompareTo(collection2));
        }

        [TestMethod]
        public void CompareTest_CollectionsDifferSlightly1()
        {
            var collection1 = dataSource.CreateNodesCollection();
            var collection2 = dataSource.CreateNodesCollection();
            var node5 = collection2.FindNodes(x => x.Tag == 5)
                .First();
            var node6 = collection2.FindNodes(x => x.Tag == 6)
                .First();
            node5.MoveTo(node6);
            Assert.IsFalse(collection1.CompareTo(collection2));
        }

        [TestMethod]
        public void CompareTest_CollectionsDifferSlightly2()
        {
            var collection1 = dataSource.CreateNodesCollection();
            var collection2 = dataSource.CreateNodesCollection();
            var node5 = collection2.FindNodes(x => x.Tag == 5).First();
            node5.Tag = 42;
            Assert.IsFalse(collection1.CompareTo(collection2));
        }

        [TestMethod]
        public void MapItemsTest()
        {
            var items = dataSource.CreateItems();
            var collection = dataSource.CreateNodesCollection();
            var mappedCollection = new NodesCollection<int>();
            mappedCollection.Map(items, x => x.Items, x => x.Value);
            Assert.IsTrue(collection.CompareTo(mappedCollection));
        }

        [TestMethod]
        public void MapFlatItemsTest()
        {
            var items = dataSource.CreateFlatItems();
            var collection = dataSource.CreateNodesCollection();
            var mappedCollection = new NodesCollection<int>();
            mappedCollection.Map(items, x => x.Id, x => x.ParentId, x => x.Id);
            Assert.IsTrue(collection.CompareTo(mappedCollection));
        }
    }
}