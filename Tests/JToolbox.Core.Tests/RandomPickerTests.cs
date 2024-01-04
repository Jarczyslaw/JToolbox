using JToolbox.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class RandomPickerTests
    {
        [TestMethod]
        public void GetNext_ReturnsNonrepeatingItems()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var randomPicker = new RandomPicker<int>(items);

            var result = new List<int>();
            var sets = 10;
            var totalResultCount = items.Count * sets;
            for (int i = 0; i < totalResultCount; i++)
            {
                result.Add(randomPicker.GetNext());
            }

            Assert.IsTrue(result.GroupBy(x => x).All(x => x.Count() == sets));

            for (int i = 0; i < sets; i++)
            {
                Assert.IsTrue(CheckListsElements(result.GetRange(i * items.Count, items.Count), items));
            }
        }

        [TestMethod]
        public void GetNext_WithPredefinedIndexes_ReturnsValidSequence()
        {
            var items = new List<int> { 1, 2, 3, 4, 5 };
            var predefinedIndexes = new List<int>() { 2, 1, 0, 2, 1, 4 };
            var randomPicker = new RandomPicker<int>(items, predefinedIndexes);

            var result = new List<int> { 3, 2, 1, 3, 2, 5 };

            foreach (var expected in result)
            {
                Assert.AreEqual(expected, randomPicker.GetNext());
            }
        }

        [TestMethod]
        public void InvalidInitialization()
        {
            Assert.ThrowsException<ArgumentException>(() => new RandomPicker<int>(null));
            Assert.ThrowsException<ArgumentException>(() => new RandomPicker<int>(new List<int> { 1, 2, 3 }, new List<int>()));
            Assert.ThrowsException<ArgumentException>(() => new RandomPicker<int>(new List<int> { 1, 2, 3 }, new List<int>() { 3 }));
        }

        private bool CheckListsElements(List<int> first, List<int> second)
        {
            return first.All(second.Contains) && first.Count == second.Count;
        }
    }
}