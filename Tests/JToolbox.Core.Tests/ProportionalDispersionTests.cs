using JToolbox.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class ProportionalDispersionTests
    {
        [TestMethod]
        public void Calculate_ReturnsValidCalculatedItems()
        {
            var dispersion = new ProportionalDispersion<int>();
            dispersion.AddItem(1, 4);
            dispersion.AddItem(2, 3);
            dispersion.AddItem(3, 2);
            dispersion.AddItem(4, 1);

            var dispersedItems = dispersion.Calculate(10);

            Assert.AreEqual(4, dispersedItems.Count);
            Assert.AreEqual(10, dispersedItems.Sum(x => x.Count));

            Assert.AreEqual(4, dispersedItems.First(x => x.Item == 1).Count);
            Assert.AreEqual(3, dispersedItems.First(x => x.Item == 2).Count);
            Assert.AreEqual(2, dispersedItems.First(x => x.Item == 3).Count);
            Assert.AreEqual(1, dispersedItems.First(x => x.Item == 4).Count);
        }

        [TestMethod]
        public void Calculate_ReturnsValidCalculatedItemsWithLowerBound()
        {
            var dispersion = new ProportionalDispersion<int>()
            {
                LowerBound = 1
            };
            dispersion.AddItem(1, 99);
            dispersion.AddItem(2, 1);

            var dispersedItems = dispersion.Calculate(1);

            Assert.AreEqual(2, dispersedItems.Count);
            Assert.AreEqual(2, dispersedItems.Sum(x => x.Count));
        }

        [TestMethod]
        public void GetQueuedItems_ReturnValidItemsCount()
        {
            var dispersion = new ProportionalDispersion<int>();
            dispersion.AddItem(1, 4);
            dispersion.AddItem(2, 3);
            dispersion.AddItem(3, 2);
            dispersion.AddItem(4, 1);

            var dispersedItems = dispersion.Calculate(10);
            var queuedItems = dispersion.GetQueuedItems(dispersedItems);

            Assert.AreEqual(10, queuedItems.Count);
            Assert.AreEqual(2, dispersedItems.Where(x => x.Item == 3).Sum(x => x.Count));
        }
    }
}