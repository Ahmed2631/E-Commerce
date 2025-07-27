using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Areas.User.Controllers
{
    [Area("User")]
    public class AddtionalPagesController : Controller
    {
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult FAQs()
        {
            return View();
        }
    }
}
