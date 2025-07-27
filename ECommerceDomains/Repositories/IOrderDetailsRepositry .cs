using ECommerceDomains.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Repositories
{
    public interface IOrderDetailsRepositry : IGenericRepositry<OrderDetails>
    {
        void Update(OrderDetails order);
    }
}
