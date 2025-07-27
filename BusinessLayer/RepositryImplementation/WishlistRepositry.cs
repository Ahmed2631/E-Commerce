using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class WishlistRepositry : GenericRepositry<WishlistCart>, IWishlistRepositry
    {
        private readonly ApplicationDbContext _context;
        public WishlistRepositry(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(WishlistCart wishlist)
        {
            // _context.TbCategories.Update(category);
            var WishList = _context.WishlistCart.FirstOrDefault(c => c.Id == wishlist.Id);
            if (WishList != null)
            {
                WishList.WishlistItems = (IEnumerable<TbPoducts>)_context.WishlistCart.Add(WishList);
                _context.SaveChanges();
            }
        }
    }
}
