using JToolbox.Core.EqualityComparers;
using JToolbox.Core.Utilities.Merge;
using JToolbox.Core.Utilities.Merge.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class MergeCollectionsTests
    {
        private readonly GenericEqualityComparer<Item, int, string> _idNameComparer = new GenericEqualityComparer<Item, int, string>(x => x.Id, x => x.Name);
        private readonly MergeCollections<Item> merge = new MergeCollections<Item>();

        [TestMethod]
        public void Merge_CollectionsContainOnlyCommonElements_AllItemsShouldBeUpdated()
        {
            var old = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                }
            };

            var @new = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                }
            };

            merge.Merge(old, @new, _idNameComparer);

            Assert.IsTrue(merge.ToUpdate.Count == old.Count);
            Assert.IsTrue(merge.ToUpdate.Select(x => x.OldItem).SequenceEqual(old));
            Assert.IsTrue(merge.ToUpdate.Select(x => x.NewItem).SequenceEqual(@new));
        }

        [TestMethod]
        public void Merge_CollectionsWithoutCommonElements_ItemsShouldBeCreatedAndDeleted()
        {
            var old = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                }
            };

            var @new = new List<Item>
            {
                new Item
                {
                    Id = 3
                },
                new Item
                {
                    Id = 4
                },
                new Item
                {
                    Id = 5
                }
            };

            merge.Merge(old, @new, _idNameComparer);

            Assert.IsTrue(merge.ToCreate.SequenceEqual(@new));
            Assert.IsTrue(merge.ToDelete.SequenceEqual(old));
        }

        [TestMethod]
        public void Merge_DifferentCollections_ItemsShouldBeCreatedUpdatedAndDeleted()
        {
            var old = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                },
                new Item
                {
                    Id = 3
                }
            };

            var @new = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                },
                new Item
                {
                    Id = 4
                },
                new Item
                {
                    Id = 5
                }
            };

            merge.Merge(old, @new, _idNameComparer);

            Assert.IsTrue(merge.ToCreate.Count == 2);
            Assert.IsTrue(merge.ToCreate.Select(x => x.Id).All(x => x == 4 || x == 5));

            Assert.IsTrue(merge.ToDelete.Count == 1);
            Assert.IsTrue(merge.ToDelete[0].Id == 3);

            Assert.IsTrue(merge.ToUpdate.Count == 2);
            Assert.IsTrue(merge.ToUpdate.Select(x => x.NewItem.Id).All(x => x == 1 || x == 2));
            Assert.IsTrue(merge.ToUpdate.Select(x => x.NewItem).All(x => @new.Contains(x)));
            Assert.IsTrue(merge.ToUpdate.Select(x => x.OldItem).All(x => old.Contains(x)));
        }

        [TestMethod]
        public void Merge_EmptyNewCollection_AllItemsShouldBeDeleted()
        {
            var old = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                }
            };

            merge.Merge(old, null, _idNameComparer);

            Assert.AreEqual(merge.ToDelete.Count, old.Count);
        }

        [TestMethod]
        public void Merge_EmptyOldCollection_AllItemsShouldBeCreated()
        {
            var @new = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 2
                }
            };

            merge.Merge(null, @new, _idNameComparer);

            Assert.AreEqual(merge.ToCreate.Count, @new.Count);
        }

        [TestMethod]
        public void Merge_InvalidOldCollection_ExceptionWillBeThrown()
        {
            var old = new List<Item>
            {
                new Item
                {
                    Id = 1
                },
                new Item
                {
                    Id = 1
                }
            };

            Assert.ThrowsException<DuplicatesFoundException>(() => merge.Merge(old, new List<Item>(), _idNameComparer));
            Assert.ThrowsException<DuplicatesFoundException>(() => merge.Merge(new List<Item>(), old, _idNameComparer));
        }

        private class Item
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}