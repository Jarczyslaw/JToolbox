using JToolbox.Core.Helpers.CollectionsMerge;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class CollectionsMergeHelperTests
    {
        [TestMethod]
        public void Merge_ComplexDifferentTypes_ReturnsValidResult()
        {
            List<TestCase<User, UserModel>> testCases = new List<TestCase<User, UserModel>>()
            {
                new TestCase<User, UserModel>()
                {
                    // items to add, update, delete and mixed duplicates
                    NewItems = new List<User>
                    {
                        new User
                        {
                            Id = 1,
                            Name = "A",
                            LastName = "A"
                        },
                        new User
                        {
                            Id = 2,
                            Name = "B",
                            LastName = "B"
                        },
                        new User
                        {
                            Id = 3,
                            Name = "B",
                            LastName = "B"
                        },
                        new User
                        {
                            Id = 4,
                            Name = "C",
                            LastName = "C"
                        }
                    },
                    OldItems = new List<UserModel>
                    {
                        new UserModel
                        {
                            Id = 5,
                            Name = "B",
                            LastName = "B"
                        },
                        new UserModel
                        {
                            Id = 6,
                            Name = "C",
                            LastName = "C"
                        },
                        new UserModel
                        {
                            Id = 7,
                            Name = "C",
                            LastName = "C"
                        },
                        new UserModel
                        {
                            Id = 8,
                            Name = "D",
                            LastName = "D"
                        },
                    },
                    ToAdd = new List<User>()
                    {
                        new User
                        {
                            Id = 1,
                            Name = "A",
                            LastName = "A"
                        },
                        new User
                        {
                            Id = 3,
                            Name = "B",
                            LastName = "B"
                        },
                    },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<User, UserModel>>()
                    {
                        new CollectionsMergeResultUpdateEntry<User, UserModel>
                        {
                            NewItem = new User
                            {
                                Id = 2,
                                Name = "B",
                                LastName = "B"
                            },
                            OldItem = new UserModel
                            {
                                Id = 5,
                                Name = "B",
                                LastName = "B"
                            },
                        },
                        new CollectionsMergeResultUpdateEntry<User, UserModel>
                        {
                            NewItem = new User
                            {
                                Id = 4,
                                Name = "C",
                                LastName = "C"
                            },
                            OldItem = new UserModel
                            {
                                Id = 6,
                                Name = "C",
                                LastName = "C"
                            },
                        },
                    },
                    ToDelete = new List<UserModel>()
                    {
                        new UserModel
                        {
                            Id = 8,
                            Name = "D",
                            LastName = "D"
                        },
                        new UserModel
                        {
                            Id = 7,
                            Name = "C",
                            LastName = "C"
                        },
                    }
                }
            };

            MergeTestCases<User, UserModel, (string, string)>(
                testCases,
                x => (x.Name, x.LastName),
                x => (x.Name, x.LastName),
                new UserEqualityComparer(),
                new UserModelEqualityComparer());
        }

        [TestMethod]
        public void Merge_PrimitiveTypes_ReturnsValidResult()
        {
            List<TestCase<int, int>> testCases = new List<TestCase<int, int>>()
            {
                // all items to add
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 3 },
                    OldItems = new List<int>(),
                    ToAdd = new List<int> { 1, 2, 3 },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>(),
                    ToDelete = new List<int>()
                },
                // all items to delete
                new TestCase<int, int>
                {
                    NewItems = new List<int>(),
                    OldItems = new List<int> { 1, 2, 3 },
                    ToAdd = new List<int>(),
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>(),
                    ToDelete = new List<int>{ 1, 2, 3 }
                },
                // nothing to merge
                new TestCase<int, int>
                {
                    NewItems = new List<int>(),
                    OldItems = new List<int>(),
                    ToAdd = new List<int>(),
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>(),
                    ToDelete = new List<int>()
                },
                // all items to update
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 3 },
                    OldItems = new List<int> { 1, 2, 3 },
                    ToAdd = new List<int>(),
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>()
                    {
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 1,
                            OldItem = 1,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 2,
                            OldItem = 2,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        }
                    },
                    ToDelete = new List<int>()
                },
                // items to add, delete and update
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 3, 4 },
                    OldItems = new List<int> { 3, 4, 5, 6 },
                    ToAdd = new List<int> { 1, 2 },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>()
                    {
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 4,
                            OldItem = 4,
                        }
                    },
                    ToDelete = new List<int> { 5, 6 }
                },
                // items to add, delete and update - the same duplicates count
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 2, 3, 3 },
                    OldItems = new List<int> { 2, 2, 3, 3, 4 },
                    ToAdd = new List<int> { 1 },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>()
                    {
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 2,
                            OldItem = 2,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 2,
                            OldItem = 2,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        }
                    },
                    ToDelete = new List<int> { 4 }
                },
                // items to add, delete and update - new items contains more duplicates than old items
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 2, 3 },
                    OldItems = new List<int> { 2, 3, 4 },
                    ToAdd = new List<int> { 1, 2 },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>()
                    {
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 2,
                            OldItem = 2,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        }
                    },
                    ToDelete = new List<int> { 4 }
                },
                // items to add, delete and update - old items contains more duplicates than new items
                new TestCase<int, int>
                {
                    NewItems = new List<int> { 1, 2, 3 },
                    OldItems = new List<int> { 2, 2, 3, 4 },
                    ToAdd = new List<int> { 1 },
                    ToUpdate = new List<CollectionsMergeResultUpdateEntry<int, int>>()
                    {
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 2,
                            OldItem = 2,
                        },
                        new CollectionsMergeResultUpdateEntry<int, int>
                        {
                            NewItem = 3,
                            OldItem = 3,
                        }
                    },
                    ToDelete = new List<int> { 2, 4 }
                }
            };

            MergeTestCases(testCases, x => x, x => x);
        }

        private void AssertCollectionsAreTheSame<T>(
            IEnumerable<T> list1,
            IEnumerable<T> list2,
            IEqualityComparer<T> comparer)
        {
            Assert.AreEqual(list1.Count(), list2.Count());
            Assert.IsTrue(list1.ToHashSet(comparer).SetEquals(list2.ToHashSet(comparer)));
        }

        private void MergeTestCases<TNew, TOld, TKey>(
                    List<TestCase<TNew, TOld>> testCases,
            Func<TNew, TKey> newItemsKeySelector,
            Func<TOld, TKey> oldItemsKeySelector,
            IEqualityComparer<TNew> newEqualityComparer = null,
            IEqualityComparer<TOld> oldEqualityComparer = null)
        {
            newEqualityComparer = newEqualityComparer ?? EqualityComparer<TNew>.Default;
            oldEqualityComparer = oldEqualityComparer ?? EqualityComparer<TOld>.Default;

            foreach (TestCase<TNew, TOld> testCase in testCases)
            {
                CollectionsMergeResult<TNew, TOld> mergeResult
                    = CollectionsMergeHelper.Merge(
                        newItems: testCase.NewItems,
                        oldItems: testCase.OldItems,
                        newItemsKeySelector: newItemsKeySelector,
                        oldItemsKeySelector: oldItemsKeySelector);

                AssertCollectionsAreTheSame(mergeResult.ToAdd, testCase.ToAdd, newEqualityComparer);

                AssertCollectionsAreTheSame(
                    mergeResult.ToUpdate.Select(x => x.NewItem),
                    testCase.ToUpdate.Select(x => x.NewItem),
                    newEqualityComparer);

                AssertCollectionsAreTheSame(
                    mergeResult.ToUpdate.Select(x => x.OldItem),
                    testCase.ToUpdate.Select(x => x.OldItem),
                    oldEqualityComparer);

                AssertCollectionsAreTheSame(mergeResult.ToDelete, testCase.ToDelete, oldEqualityComparer);
            }
        }

        private class TestCase<TNew, TOld>
        {
            public List<TNew> NewItems { get; set; }

            public List<TOld> OldItems { get; set; }

            public List<TNew> ToAdd { get; set; }

            public List<TOld> ToDelete { get; set; }

            public List<CollectionsMergeResultUpdateEntry<TNew, TOld>> ToUpdate { get; set; }
        }

        private class User : IEquatable<User>
        {
            public int Id { get; set; }

            public string LastName { get; set; }

            public string Name { get; set; }

            public bool Equals(User other)
            {
                return other.LastName == LastName && other.Name == Name;
            }
        }

        private class UserEqualityComparer : IEqualityComparer<User>
        {
            public bool Equals(User x, User y)
            {
                return x.LastName == y.LastName && x.Name == y.Name;
            }

            public int GetHashCode(User obj)
            {
                return HashCode.Combine(obj.LastName, obj.Name);
            }
        }

        private class UserModel : IEquatable<UserModel>
        {
            public int Id { get; set; }

            public string LastName { get; set; }

            public string Name { get; set; }

            public bool Equals(UserModel other)
            {
                return other.LastName == LastName && other.Name == Name;
            }
        }

        private class UserModelEqualityComparer : IEqualityComparer<UserModel>
        {
            public bool Equals(UserModel x, UserModel y)
            {
                return x.LastName == y.LastName && x.Name == y.Name;
            }

            public int GetHashCode(UserModel obj)
            {
                return HashCode.Combine(obj.LastName, obj.Name);
            }
        }
    }
}