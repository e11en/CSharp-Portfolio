using System.Collections.Generic;
using Data.Enum;

namespace Data
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public OrderStatus Status { get; set; }
    }
}
