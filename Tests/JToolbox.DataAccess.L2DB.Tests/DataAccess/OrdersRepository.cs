using JToolbox.Core.TimeProvider;
using JToolbox.DataAccess.L2DB.Repositories;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    internal class OrdersRepository : BaseExtendedRepository<Order>
    {
        public OrdersRepository(ITimeProvider timeProvider)
            : base(timeProvider)
        {
        }
    }
}