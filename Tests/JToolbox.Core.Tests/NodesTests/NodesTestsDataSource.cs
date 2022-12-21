using JToolbox.Core.Models.Nodes;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.NodesTests
{
    /* Structure:
         *  1
	            2
	            3
		            4
		            5
	            6
            7
            8
	            9
		            10
	            11
		            12
	            13
		            14
		            15
			            16
         */

    public class NodesTestsDataSource
    {
        public List<FlatItem> CreateFlatItems()
        {
            return new List<FlatItem>()
            {
                new FlatItem(1, 0),
                new FlatItem(2, 1),
                new FlatItem(3, 1),
                new FlatItem(4, 3),
                new FlatItem(5, 3),
                new FlatItem(6, 1),
                new FlatItem(7, 0),
                new FlatItem(8, 0),
                new FlatItem(9, 8),
                new FlatItem(10, 9),
                new FlatItem(11, 8),
                new FlatItem(12, 11),
                new FlatItem(13, 8),
                new FlatItem(14, 13),
                new FlatItem(15, 13),
                new FlatItem(16, 15),
            };
        }

        public List<Item> CreateItems()
        {
            return new List<Item>
            {
                new Item(1)
                {
                    Items = new List<Item>
                    {
                        new Item(2),
                        new Item(3)
                        {
                            Items = new List<Item>
                            {
                                new Item(4),
                                new Item(5)
                            }
                        },
                        new Item(6)
                    }
                },
                new Item(7),
                new Item(8)
                {
                    Items = new List<Item>
                    {
                        new Item(9)
                        {
                            Items = new List<Item>
                            {
                                new Item(10)
                            }
                        },
                        new Item(11)
                        {
                            Items = new List<Item>
                            {
                                new Item(12)
                            }
                        },
                        new Item(13)
                        {
                            Items = new List<Item>
                            {
                                new Item(14),
                                new Item(15)
                                {
                                    Items = new List<Item>
                                    {
                                        new Item(16)
                                    }
                                }
                            }
                        },
                    }
                },
            };
        }

        public NodesCollection<int> CreateNodesCollection()
        {
            var collection = new NodesCollection<int>();
            var node1 = collection.AddNewNode(1);
            node1.AddNewNode(2);
            var node3 = node1.AddNewNode(3);
            node3.AddNewNodes(new List<int> { 4, 5 });
            node1.AddNewNode(6);
            collection.AddNewNode(7);
            var node8 = collection.AddNewNode(8);
            var node9 = node8.AddNewNode(9);
            node9.AddNewNode(10);
            var node11 = node8.AddNewNode(11);
            node11.AddNewNode(12);
            var node13 = node8.AddNewNode(13);
            node13.AddNewNode(14);
            var node15 = node13.AddNewNode(15);
            node15.AddNewNode(16);
            return collection;
        }

        public class FlatItem
        {
            public FlatItem(int id, int parentId)
            {
                Id = id;
                ParentId = parentId;
            }

            public int Id { get; set; }

            public int ParentId { get; set; }
        }

        public class Item
        {
            public Item(int value)
            {
                Value = value;
            }

            public List<Item> Items { get; set; } = new List<Item>();

            public int Value { get; set; }
        }
    }
}