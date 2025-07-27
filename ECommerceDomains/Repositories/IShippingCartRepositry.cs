using ECommerceDomains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Repositories
{
    public interface IShippingCartRepositry : IGenericRepositry<ShoppingCart>
    {
        int InCreaseShopingCart(ShoppingCart detailsAndShoppingCart, int quantity );
        int DeCreaseShopingCart(ShoppingCart detailsAndShoppingCart, int quantity );
    }
}
