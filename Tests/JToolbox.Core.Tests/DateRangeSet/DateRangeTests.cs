using JToolbox.Core.Extensions;
using JToolbox.Core.Models.DateRanges;
using JToolbox.Core.Tests.DateRangeSet.TestCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace JToolbox.Core.Tests.DateRangeSet
{
    [TestClass]
    public class DateRangeTests
    {
        private readonly DateRangeComparer _comparer = new DateRangeComparer();

        [TestMethod]
        public void GetDifference_ReturnsValidValue()
        {
            GetDifferenceTestCasesSource.GetDifferentTestCases().ForEach(x =>
            {
                List<DateRange> result = x.Range1.GetDifference(x.Range2);

                Assert.IsTrue(result.ScrambledEquals(x.Result, _comparer));
            });
        }

        [TestMethod]
        public void GetIntersection_ReturnsValidValue()
        {
            GetIntersectionTestCasesSource.GetIntersectionTestCases().ForEach(x =>
            {
                DateRange result = x.Range1.GetIntersection(x.Range2);

                Assert.IsTrue(x.Result == result);
            });
        }

        [TestMethod]
        public void Merge_ReturnsValidValue()
        {
            MergeTestCasesSource.MergeTestCases().ForEach(x =>
            {
                DateRange result = x.Range1.Merge(x.Range2);

                Assert.IsTrue(x.Result == result);
            });
        }
    }
}