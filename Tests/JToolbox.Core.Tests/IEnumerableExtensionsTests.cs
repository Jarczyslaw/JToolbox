using JToolbox.Core.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class IEnumerableExtensionsTests
    {
        [TestMethod]
        public void SearchRecursively_GetItemsWithIdsBetween()
        {
            var items = GetItems();

            var result = new List<Item>();
            items.SearchRecursively(x => x.Items, x => x.Id >= 5 && x.Id <= 8, result);

            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void SearchRecursively_GetItemWithGivenId()
        {
            var items = GetItems();

            var id = 6;
            var foundItems = items.SearchRecursively(x => x.Items, x => x.Id == id);

            Assert.AreEqual(1, foundItems.Count);
            Assert.AreEqual(id, foundItems.Single().Id);
        }

        private List<Item> GetItems()
        {
            return new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = 2,
                            Items = new List<Item>
                            {
                                new Item
                                {
                                    Id = 3
                                }
                            }
                        },
                        new Item
                        {
                            Id = 4,
                        },
                        new Item
                        {
                            Id = 5
                        }
                    }
                },
                new Item
                {
                    Id = 6
                },
                new Item
                {
                    Id = 7,
                    Items = new List<Item>
                    {
                        new Item
                        {
                            Id = 8,
                            Items = new List<Item>
                            {
                                new Item
                                {
                                    Id = 9
                                }
                            }
                        },
                        new Item
                        {
                            Id = 10,
                            Items = new List<Item>
                            {
                                new Item
                                {
                                    Id = 11
                                }
                            }
                        },
                    }
                },
            };
        }

        private class Item
        {
            public int Id { get; set; }
            public List<Item> Items { get; set; }
        }
    }
}