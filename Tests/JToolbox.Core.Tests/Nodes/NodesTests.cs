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
        public void AddExistingNodeTest()
        {
            var collection = dataSource.CreateNodesCollection();
            var node3 = collection.FindNode(x => x.Tag == 3);
            var node13 = collection.FindNode(x => x.Tag == 13);

            Assert.AreEqual(1, node3.Parent.Tag);
            Assert.AreEqual(8, node13.Parent.Tag);

            node13.AddNode(node3);
            Assert.AreEqual(node13.Tag, node3.Parent.Tag);
            Assert.IsTrue(node13.Nodes.Contains(node3));
            Assert.AreEqual(6, node13.AllNodesCount);
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
            node5.AddNode(node6);
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
        public void CreateNodesCollectionTest()
        {
            var collection = dataSource.CreateNodesCollection();
            Assert.AreEqual(3, collection.NodesCount);
            Assert.AreEqual(16, collection.AllNodesCount);
        }

        [TestMethod]
        public void GetAllNodesTest()
        {
            var collection = dataSource.CreateNodesCollection();
            var allNodes = collection.GetAllNodes();
            Assert.AreEqual(16, allNodes.Count);
        }

        [TestMethod]
        public void GetAllParentsTest()
        {
            var collection = dataSource.CreateNodesCollection();
            var node16 = collection.FindNode(x => x.Tag == 16);
            Assert.AreEqual(3, node16.GetAllParents().Count);
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
        public void RemoveNodesTest()
        {
            var collection = dataSource.CreateNodesCollection();
            var node3 = collection.FindNode(x => x.Tag == 3);
            collection.RemoveNode(node3);
            Assert.AreEqual(13, collection.AllNodesCount);

            var node13 = collection.FindNode(x => x.Tag == 13);
            var node15 = collection.FindNode(x => x.Tag == 15);
            node13.RemoveNode(node15);
            Assert.AreEqual(11, collection.AllNodesCount);
        }
    }
}