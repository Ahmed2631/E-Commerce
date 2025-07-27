using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class ApplicationUserRepositry : GenericRepositry<ApplicationUser>, IApplicationUserRepositry
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }
    }
}
