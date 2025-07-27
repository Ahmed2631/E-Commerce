using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context, ICategoryRepositry category , IShippingCartRepositry shippingCart)
        {
            _context = context;
            // Ai repositry is injected here
            Category = category;
            Product = new ProductRepositry(context);
            ShippingCart = shippingCart;
            OrderDetails = new OrderDetailsRepositry(context);
            OrderHeader = new OrderHeaderRepositry(context);
            ApplicationUserRepositry = new ApplicationUserRepositry(context);
            Wishlist = new WishlistRepositry(context);
        }

        public ICategoryRepositry Category { get; private set; }
        public IProductRepositry Product { get; private set; }
        public IShippingCartRepositry ShippingCart { get; private set; }
        public IOrderDetailsRepositry OrderDetails { get; private set; }
        public IOrderHeaderRepositry OrderHeader { get; private set; }
        public IApplicationUserRepositry ApplicationUserRepositry { get; private set; }
        public IWishlistRepositry Wishlist { get; private set; }

        public int complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
             _context.Dispose();
        }
    }
}
