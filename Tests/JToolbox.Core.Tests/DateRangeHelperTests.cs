using JToolbox.Core.Extensions;
using JToolbox.Core.Helpers;
using JToolbox.Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class DateRangeHelperTests
    {
        [TestMethod]
        public void Intersection_InvalidInputRanges_ShouldThrowArgumentException()
        {
            var range1 = new List<DateRange>()
            {
                GetRange(1, 5),
                GetRange(4, 10)
            };

            var range2 = new List<DateRange>()
            {
                GetRange(1, 2)
            };
            Assert.ThrowsException<ArgumentException>(() => DateRangeHelper.Intersection(range1, range2, false));
        }

        [TestMethod]
        public void Intersection_ValidInputTestCases_ShouldReturnValidResult()
        {
            var testCases = new List<TestCase>
            {
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16) },
                    Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(1, 2), },
                    Results = new List<DateRange>()
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(1, 5), },
                    Results = new List<DateRange> { GetRange(3, 5) }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(4, 5), GetRange(12, 13), },
                    Results = new List<DateRange> { GetRange(4, 5) }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(4, 7), },
                    Results = new List<DateRange> { GetRange(4, 6) }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(5, 9), },
                    Results = new List<DateRange> { GetRange(5, 6), GetRange(8, 9) }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(9, 10), },
                    Results = new List<DateRange> { GetRange(9, 10), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(10, 12), },
                    Results = new List<DateRange> { GetRange(10, 11), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(10, 14), },
                    Results = new List<DateRange> { GetRange(10, 11), GetRange(13, 14), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(12, 14), },
                    Results = new List<DateRange> { GetRange(13, 14), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(14, 15), },
                    Results = new List<DateRange> { GetRange(14, 15), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(14, 18), },
                    Results = new List<DateRange> { GetRange(14, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(17, 20), },
                    Results = new List<DateRange>()
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(2, 9), },
                    Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 9), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(7, 15), },
                    Results = new List<DateRange> { GetRange(8, 11), GetRange(13, 15), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(3, 16), },
                    Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(2, 17), },
                    Results = new List<DateRange> { GetRange(3, 6), GetRange(8, 11), GetRange(13, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(4, 7), GetRange(9, 12), GetRange(14, 17), },
                    Results = new List<DateRange> { GetRange(4, 6), GetRange(9, 11), GetRange(14, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(6, 8), GetRange(11, 13), GetRange(16, 18), },
                    Results = new List<DateRange>()
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(7, 9), GetRange(10, 14), GetRange(15, 18), },
                    Results = new List<DateRange> { GetRange(8, 9), GetRange(10, 11), GetRange(13, 14), GetRange(15, 16), }
                },
                new TestCase
                {
                    Ranges2 = new List<DateRange> { GetRange(10, 17), },
                    Results = new List<DateRange> { GetRange(10, 11), GetRange(13, 16), }
                },
            };

            var comparer = new DateRangeComparer();
            testCases.ForEach(x =>
            {
                var result = DateRangeHelper.Intersection(x.Ranges1, x.Ranges2, false);
                Assert.IsTrue(x.Results.ScrambledEquals(result, comparer));
            });
        }

        private static DateRange GetRange(int from, int to)
        {
            return new DateRange(new DateTime(from, 1, 1), new DateTime(to, 1, 1));
        }

        private class TestCase
        {
            public List<DateRange> Ranges1 { get; set; } = new List<DateRange>
            {
                GetRange(3, 6),
                GetRange(8, 11),
                GetRange(13, 16),
            };

            public List<DateRange> Ranges2 { get; set; }

            public List<DateRange> Results { get; set; }
        }
    }
}