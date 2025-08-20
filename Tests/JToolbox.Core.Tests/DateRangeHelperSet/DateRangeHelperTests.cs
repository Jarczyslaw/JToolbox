using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeHelperSet.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeHelperSet
{
    [TestClass]
    public class DateRangeHelperTests
    {
        private readonly DateRangeComparer comparer = new DateRangeComparer();

        [TestMethod]
        public void GetComplementary_ValidInputTestCases_ShouldReturnValidResult()
        {
            GetComplementaryTestCasesSource.GetComplementaryTestCases.ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.GetComplementary(x.Ranges);

                Assert.IsTrue(x.Results.ScrambledEquals(result, comparer));
                AssertOrderAndOverlapping(result);
            });
        }

        [TestMethod]
        public void GetDifference_ValidInputTestCases_ShouldReturnValidResult()
        {
            GetDifferenceTestCasesSource.GetDifferenceTestCases.ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.GetDifference(x.Ranges1, x.Ranges2);

                Assert.IsTrue(x.Results.ScrambledEquals(result, comparer));
                AssertOrderAndOverlapping(result);
            });
        }

        [TestMethod]
        public void GetIntersection_ValidInputTestCases_ShouldReturnValidResult()
        {
            GetIntersectionTestCasesSource.GetIntersectionTestCases.ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.GetIntersection(x.Ranges1, x.Ranges2);

                Assert.IsTrue(x.Results.ScrambledEquals(result, comparer));
                AssertOrderAndOverlapping(result);
            });
        }

        [TestMethod]
        public void Merge_ValidInputTestCases_ShouldReturnValidResult()
        {
            MergeTestCasesSource.MergeTestCases.ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.Merge(x.Input);

                Assert.IsTrue(x.Output.ScrambledEquals(result, comparer));
                AssertOrderAndOverlapping(result);
            });
        }

        [TestMethod]
        public void SplitByDate_TestCases_ShouldReturnValidResult()
        {
            SplitByDateTestCasesSource.SplitByDateTestCases.ForEach(x =>
            {
                (List<DateRange> leftSide, List<DateRange> rightSide) = DateRangeHelper.SplitByDate(x.Ranges, x.Date);
                Assert.IsTrue(leftSide.ScrambledEquals(x.LeftSide, comparer));
                Assert.IsTrue(rightSide.ScrambledEquals(x.RightSide, comparer));
            });
        }

        [TestMethod]
        public void Trim_ValidInputTestCases_ShouldReturnValidResult()
        {
            TrimTestCasesSource.TrimTestCases().ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.Trim(x.Ranges, x.Start, x.End);

                Assert.IsTrue(x.Result.ScrambledEquals(result, comparer));
            });
        }

        [TestMethod]
        public void TrimEnd_ValidInputTestCases_ShouldReturnValidResult()
        {
            TrimTestCasesSource.TrimEndTestCases().ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.TrimEnd(x.Ranges, x.Date);

                Assert.IsTrue(x.Result.ScrambledEquals(result, comparer));
            });
        }

        [TestMethod]
        public void TrimStart_ValidInputTestCases_ShouldReturnValidResult()
        {
            TrimTestCasesSource.TrimStartTestCases().ForEach(x =>
            {
                List<DateRange> result = DateRangeHelper.TrimStart(x.Ranges, x.Date);

                Assert.IsTrue(x.Result.ScrambledEquals(result, comparer));
            });
        }

        private void AssertOrderAndOverlapping(List<DateRange> ranges)
        {
            Assert.IsTrue(DateRangeHelper.IsOrdered(ranges));
            Assert.IsFalse(DateRangeHelper.CheckInternalOverlappingOrdered(ranges));
        }
    }
}