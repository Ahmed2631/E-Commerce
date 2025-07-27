using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class OrderHeaderRepositry : GenericRepositry<OrderHeader>, IOrderHeaderRepositry
    {
        private readonly ApplicationDbContext _context;
        public OrderHeaderRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }
        public void Update(OrderHeader order)
        {
           _context.TbOrderHeader.Update(order);
        }
        public void UpdateOrderStatus(int id, string orderStatus, string? paymentStatus)
        {
            var orderFromDb = _context.TbOrderHeader.FirstOrDefault(u => u.Id == id);
            if (orderFromDb != null)
            {
                orderFromDb.OrderStatus = orderStatus;
                orderFromDb.PaymentDate = DateTime.Now;
                if (paymentStatus != null)
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
