using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class ShoppingCartVm
    {
        public IEnumerable<ShoppingCart> LstShoppingCart { get; set; } = new List<ShoppingCart>();
        public OrderHeader OrderHeader { get; set; }
        public decimal ShoppingCartTotal { get; set; } = 0;
        public string? Coupon { get; set; } 
    }
}
