using ECommerce.Helper;
using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = TbRoles.Admin)]
    [Authorize(Roles = "Admin")]
    public class OrderDetailsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderConfirmationDetailsVm order { get; set; } 
        public OrderDetailsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AllOrders()
        {
            IEnumerable<OrderHeader> orderHeaders = _unitOfWork.OrderHeader.GetAll(word:"ApplicationUser"); // Include ApplicationUser Data
            return Json(new { data = orderHeaders });
        }

        // For Details Order
        public IActionResult Details(int orderId)
        {
            OrderConfirmationDetailsVm orderVm = new OrderConfirmationDetailsVm()
            {
                orderHeader = _unitOfWork.OrderHeader.GetFristOrDefault(a=> a.Id==orderId , word: "ApplicationUser"),
                orderDetails = _unitOfWork.OrderDetails.GetAll(z => z.OrderHeaderId == orderId ,word : "Product"),
            };

            return View(orderVm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details()
        {
            var orderHeader = _unitOfWork.OrderHeader.GetFristOrDefault(a => a.Id == order.orderHeader.Id);
            orderHeader.UserName = order.orderHeader.UserName;
            orderHeader.Adress = order.orderHeader.Adress;
            orderHeader.City = order.orderHeader.City;
            orderHeader.PhoneNumber = order.orderHeader.PhoneNumber;

            if(order.orderHeader.Carrier != "" || order.orderHeader.Carrier != null || order.orderHeader.Carrier != "Waiting For Carrier")
                orderHeader.Carrier = order.orderHeader.Carrier;

            if(order.orderHeader.TrackingNum != null)
                orderHeader.TrackingNum = order.orderHeader.TrackingNum;


            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.complete();

            TempData["OrderUpdate"] = "Order Status Has Updated Successfully";
            return RedirectToAction("Index" , "OrderDetails");
        }


       //  For Start Proccessing Order
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartProccess()
        {
            _unitOfWork.OrderHeader.UpdateOrderStatus(order.orderHeader.Id, SolidStatement.Proccessing , SolidStatement.Approve );
            _unitOfWork.complete();

            TempData["OrderUpdate"] = "Order Status Has Updated Successfully";
            return RedirectToAction("Details", "OrderDetails" , new {orderId = order.orderHeader.Id});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StartShip()
        {
            var orderHeader = _unitOfWork.OrderHeader.GetFristOrDefault(a => a.Id == order.orderHeader.Id);
            orderHeader.TrackingNum = order.orderHeader.TrackingNum;
            orderHeader.Carrier = order.orderHeader.Carrier;
            orderHeader.OrderStatus = SolidStatement.Shipped;
            orderHeader.ShippingDate = DateTime.Now;

            _unitOfWork.OrderHeader.Update(orderHeader);
            _unitOfWork.complete();

            TempData["OrderUpdate"] = "Order Has Shipped Successfully";
            return RedirectToAction("Details", "OrderDetails", new { orderId = order.orderHeader.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CancelOrder()
        {
            var orderHeader = _unitOfWork.OrderHeader.GetFristOrDefault(a => a.Id == order.orderHeader.Id);
            if(orderHeader.PaymentStatus == SolidStatement.Approve)
            {
                // If the order is already paid, we need to refund the payment
                orderHeader.OrderStatus = SolidStatement.Refund;
                orderHeader.PaymentStatus = SolidStatement.Refund;

                // Create a refund using Stripe
                var option = new RefundCreateOptions
                {
                    Reason = RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.PaymentInteniD
                };
                var service = new RefundService();
                Refund refund = service.Create(option);
                _unitOfWork.OrderHeader.UpdateOrderStatus(orderHeader.Id, SolidStatement.Cancelled, SolidStatement.Refund);
            }
            else
            {
                // If the order is not paid, we can just cancel it
                orderHeader.OrderStatus = SolidStatement.Cancelled;
                _unitOfWork.OrderHeader.UpdateOrderStatus(orderHeader.Id, SolidStatement.Cancelled, SolidStatement.Cancelled);
            }
            _unitOfWork.complete();

            TempData["OrderDeleted"] = "Order Has Deleted Successfully";
            return RedirectToAction("Details", "OrderDetails", new { orderId = order.orderHeader.Id });
        }
    }
}
