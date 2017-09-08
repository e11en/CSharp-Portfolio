using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
