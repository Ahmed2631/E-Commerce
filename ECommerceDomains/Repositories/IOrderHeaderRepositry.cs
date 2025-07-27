using ECommerceDomains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Repositories
{
    public interface IOrderHeaderRepositry : IGenericRepositry<OrderHeader>
    {
        void Update(OrderHeader order);
        void UpdateOrderStatus(int id,  string orderStatus , string? paymentStatus);
    }
}
