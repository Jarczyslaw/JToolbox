using JToolbox.Core.Helpers.Retry;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace JToolbox.Core.Tests
{
    [TestClass]
    public class RetryHelperTests
    {
        [TestMethod]
        public async Task ExceptionInAllAttempts()
        {
            int counter = 0;

            RetryResult<bool> retryResult = await RetryHelper.TryAsync(
                () => TestAction(ref counter, 10, 0),
                new RetryArgs<bool>
                {
                    BreakHandler = BreakHandler
                });

            Assert.IsFalse(retryResult.LastResult);
            Assert.AreEqual(5, retryResult.Attempt);
            Assert.IsNotNull(retryResult.LastException);
        }

        [TestMethod]
        public async Task NoSuccessInAllAttempts()
        {
            int counter = 0;

            RetryResult<bool> retryResult = await RetryHelper.TryAsync(
                () => TestAction(ref counter, 10),
                new RetryArgs<bool>
                {
                    BreakHandler = BreakHandler
                });

            Assert.IsFalse(retryResult.LastResult);
            Assert.AreEqual(5, retryResult.Attempt);
            Assert.IsNull(retryResult.LastException);
        }

        [TestMethod]
        public async Task SuccessAfterThreeAttempts()
        {
            int counter = 0;

            RetryResult<bool> retryResult = await RetryHelper.TryAsync(
                () => TestAction(ref counter, 3, 5),
                new RetryArgs<bool>
                {
                    BreakHandler = BreakHandler
                });

            Assert.IsTrue(retryResult.LastResult);
            Assert.AreEqual(3, retryResult.Attempt);
            Assert.IsNull(retryResult.LastException);
        }

        [TestMethod]
        public async Task SuccessAtFirstAttempt()
        {
            int counter = 0;

            RetryResult<bool> retryResult = await RetryHelper.TryAsync(
                () => TestAction(ref counter, 1),
                new RetryArgs<bool>
                {
                    BreakHandler = BreakHandler
                });

            Assert.IsTrue(retryResult.LastResult);
            Assert.AreEqual(1, retryResult.Attempt);
            Assert.IsNull(retryResult.LastException);
        }

        private bool BreakHandler(RetryResult<bool> result)
        {
            if (result.LastException != null) { return false; }

            return result.LastResult;
        }

        private Task<bool> TestAction(
            ref int currentValue,
            int successValue,
            int? exceptionThreshold = null)
        {
            currentValue++;

            if (currentValue >= exceptionThreshold) { throw new Exception(); }

            return Task.FromResult(currentValue == successValue);
        }
    }
}