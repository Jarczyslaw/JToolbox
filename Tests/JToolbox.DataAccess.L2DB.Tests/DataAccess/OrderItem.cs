using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB.Mapping;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    [Table("OrderItems")]
    public class OrderItem : BaseEntity
    {
        [Column, NotNull]
        public string Name { get; set; }

        [Association(ThisKey = nameof(OrderId), OtherKey = nameof(Order.Id))]
        public Order Order { get; set; }

        [Column, NotNull]
        public int OrderId { get; set; }
    }
}