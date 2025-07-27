using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class ShippingCartRepositry : GenericRepositry<ShoppingCart>, IShippingCartRepositry
    {
        private readonly ApplicationDbContext _context;
        public ShippingCartRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }

        public int DeCreaseShopingCart(ShoppingCart shoppingCart, int quantity)
        {
            shoppingCart.Quantity -= quantity;
            return shoppingCart.Quantity;
        }

        public int InCreaseShopingCart(ShoppingCart shoppingCart, int quantity)
        {
            shoppingCart.Quantity += quantity;
            return shoppingCart.Quantity;
        }
    }
}
