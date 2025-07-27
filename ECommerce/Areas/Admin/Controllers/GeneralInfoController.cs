using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.RepositryImplementation;
using ECommerceDomains.Repositories;

namespace ECommerce.Areas.Admin.Controllers
{
    //[Authorize(Roles = TbRoles.Admin)]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class GeneralInfoController : Controller
    {
        private IUnitOfWork _unit;
        public GeneralInfoController(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IActionResult Index()
        {
            ViewBag.Order = _unit.OrderHeader.GetAll().Count();
            ViewBag.AllUsers = _unit.ApplicationUserRepositry.GetAll().Count();
            ViewBag.ApprovedOrder = _unit.OrderHeader.GetAll().Where(o => o.OrderStatus == "Approved").Count();

            ViewBag.PendingOrder = _unit.OrderHeader.GetAll().Where(o => o.OrderStatus == "Pending").Count();
            ViewBag.ProccessingOrder = _unit.OrderHeader.GetAll().Where(o => o.OrderStatus == "Proccessing").Count();
            ViewBag.CancelledOrder = _unit.OrderHeader.GetAll().Where(o => o.OrderStatus == "Cancelled").Count();
            ViewBag.ShippedOrder = _unit.OrderHeader.GetAll().Where(o => o.OrderStatus == "Shipped").Count();

            return View();
        }
    }
}
