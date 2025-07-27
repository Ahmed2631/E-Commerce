using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepositry Category{ get; }
        IProductRepositry Product { get; }
        IShippingCartRepositry ShippingCart { get; }
        IOrderDetailsRepositry OrderDetails { get; }
        IOrderHeaderRepositry OrderHeader { get; }
        IApplicationUserRepositry ApplicationUserRepositry { get; }
        IWishlistRepositry Wishlist { get; }

        // For Save.Changes();
        int complete();
    }
}
