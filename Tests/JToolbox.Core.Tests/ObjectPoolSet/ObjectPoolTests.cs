using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JToolbox.Core.Tests.ObjectPoolSet
{
    [TestClass]
    public class ObjectPoolTests
    {
        [TestMethod]
        public async Task Get_InstancesLimitExceeded()
        {
            DbContextPool pool = new DbContextPool();

            List<DbContext> instances = new List<DbContext>();

            for (int i = 0; i < pool.MaxInstancesCount + 1; i++)
            {
                instances.Add(await pool.Get());
            }

            foreach (DbContext instance in instances)
            {
                await pool.Return(instance);
            }

            Assert.AreEqual(1, pool.PoolLimitExceededTimes);
        }

        [TestMethod]
        public async Task GetReturn_RetrieveTheSameInstance()
        {
            DbContextPool pool = new DbContextPool();

            DbContext instance = await pool.Get();

            Assert.IsTrue(instance.IsInUse);

            await pool.Return(instance);

            Assert.IsFalse(instance.IsInUse);

            DbContext anotherInstance = await pool.Get();

            Assert.AreEqual(instance.Id, anotherInstance.Id);
        }
    }
}