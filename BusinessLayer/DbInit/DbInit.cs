using ECommerce.Helper;
using ECommerceDomains.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DbInit
{
    public class DbInit : IDbInit
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public DbInit(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        public async Task InitializeDatabaseAsync()
        {
            // Migrations
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            // Roles
            if (!_roleManager.RoleExistsAsync(TbRoles.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(TbRoles.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(TbRoles.Editor)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(TbRoles.Customer)).GetAwaiter().GetResult();

                // Users

                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "Ahmed254010@ECommerce.com",
                    Email = "Ahmed254010@ECommerce.com",
                    PhoneNumber = "1234567890",
                    Adress = "123 Mansoura St",
                    City = "Sample City"
                }, "Ah2540$$").GetAwaiter().GetResult();

                ApplicationUser user = _context.DBApplicationUser.FirstOrDefault(u => u.Email == "Ahmed@Ecommerce.com");

                _userManager.AddToRoleAsync(user, TbRoles.Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }


}
