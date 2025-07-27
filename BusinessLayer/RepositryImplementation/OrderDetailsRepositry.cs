using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class OrderDetailsRepositry : GenericRepositry<OrderDetails>, IOrderDetailsRepositry
    {
        private readonly ApplicationDbContext _context;
        public OrderDetailsRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }
        public void Update(OrderDetails order)
        {
            _context.TbOrderDetails.Update(order);
        }
    }
}
