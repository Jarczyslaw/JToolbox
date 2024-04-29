using JToolbox.DataAccess.L2DB.Entities;
using LinqToDB.Mapping;
using System.ComponentModel;

namespace JToolbox.DataAccess.L2DB.Tests.DataAccess
{
    public enum OrderType
    {
        Normal,
        Advanced
    }

    [Table("Orders")]
    public class Order : BaseExtendedEntity
    {
        [Column, NotNull, DefaultValue(true)]
        public bool IsActive { get; set; }

        [Association(ThisKey = nameof(Order.Id), OtherKey = nameof(OrderItem.OrderId))]
        public List<OrderItem> Items { get; set; }

        [Column, NotNull]
        public string Name { get; set; }

        [Column, NotNull]
        public OrderType OrderType { get; set; }
    }
}