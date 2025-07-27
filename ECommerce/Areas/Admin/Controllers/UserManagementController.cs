using BusinessLayer;
using ECommerce.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //  [Authorize(Roles = TbRoles.Admin)]
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserManagementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult AllUser()
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);
            string userId = claims.Value;
            var user = _context.DBApplicationUser.Where(u => u.Id != userId).ToList();

            return View(user);
        }
        public IActionResult DeleteAcc(string? UserId)
        {
            var user = _context.DBApplicationUser.FirstOrDefault(u => u.Id == UserId);
            if (user == null)
            {
                return NotFound();
            }
            _context.DBApplicationUser.Remove(user);
            _context.SaveChanges();
            return RedirectToAction("AllUser", "UserManagement", new { area = "Admin" });
        }    // Socaial Media Auth
        public IActionResult LockUnlock(string? UserId)
        {
            var user = _context.DBApplicationUser.FirstOrDefault(u => u.Id == UserId);
            if (user == null)
            {
                return NotFound();
            }
            if (user.LockoutEnd == null || user.LockoutEnd == DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now.AddYears(1); // Lock the user for 1 years
            }
            else
            {
              //  user.LockoutEnd = DateTime.Now; // Unlock the user
                user.LockoutEnd = null; // Unlock the user
            }
            _context.SaveChanges();
            return RedirectToAction("AllUser", "UserManagement", new { area = "Admin" });
        }
    }
}

