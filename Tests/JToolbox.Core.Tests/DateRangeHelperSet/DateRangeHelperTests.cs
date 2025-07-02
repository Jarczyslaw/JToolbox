using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet
{
    [TestClass]
    public class DateRangeHelperTests
    {
        private readonly DateRangeComparer comparer = new DateRangeComparer();

        [TestMethod]
        public void Intersection_InvalidInputRanges_ShouldThrowArgumentException()
        {
            var range1 = new List<DateRange>()
            {
                TestCasesSource.GetRange(1, 5),
                TestCasesSource.GetRange(4, 10)
            };

            var range2 = new List<DateRange>()
            {
                TestCasesSource.GetRange(1, 2)
            };

            Assert.ThrowsException<ArgumentException>(() => DateRangeHelper.Intersection(range1, range2, false));
        }

        [TestMethod]
        public void Intersection_ValidInputTestCases_ShouldReturnValidResult()
        {
            TestCasesSource.IntersectionTestCases.ForEach(x =>
            {
                var result = DateRangeHelper.Intersection(x.Ranges1, x.Ranges2, false);
                Assert.IsTrue(x.Results.ScrambledEquals(result, comparer));
            });
        }

        [TestMethod]
        public void Merge_ValidInputTestCases_ShouldReturnValidResult()
        {
            TestCasesSource.MergeTestCases.ForEach(x =>
            {
                var result = DateRangeHelper.Merge(x.Input, false);
                Assert.IsTrue(x.Output.ScrambledEquals(result, comparer));
            });
        }

        [TestMethod]
        public void SplitByDate_TestCases_ShouldReturnValidResult()
        {
            TestCasesSource.SplitByDateTestCases.ForEach(x =>
            {
                (List<DateRange> leftSide, List<DateRange> rightSide) = DateRangeHelper.SplitByDate(x.Ranges, x.Date);
                Assert.IsTrue(leftSide.ScrambledEquals(x.LeftSide, comparer));
                Assert.IsTrue(rightSide.ScrambledEquals(x.RightSide, comparer));
            });
        }
    }
}