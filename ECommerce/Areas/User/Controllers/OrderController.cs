using ECommerce.Helper;
using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;
using System.Security.Claims;

namespace ECommerce.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVm shoppingCartVm { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // I know That is not "Clean Code" and Not "Best Practice" But I Will Improve that when I Finish the Web App.
        public IActionResult ShoppingCart(ShoppingCart shoppingCart, int? ProductId)
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claims.Value;

            var product = _unitOfWork.Product.GetFristOrDefault(p => p.Id == shoppingCart.ProductId);
            if(product != null)
            {
                shoppingCart.TbProducts = product;
                shoppingCart.ProductId = product.Id;

                ShoppingCart CartObj = _unitOfWork.ShippingCart.GetFristOrDefault(
                    u => (u.ApplicationUserId == claims.Value) && (u.ProductId == shoppingCart.ProductId));

                if (CartObj == null)
                {
                    _unitOfWork.ShippingCart.Add(shoppingCart);
                    
                }
                else
                {
                    _unitOfWork.ShippingCart.InCreaseShopingCart(CartObj, shoppingCart.Quantity);
                }
            }
            else
            {
                var productDiv = _unitOfWork.Product.GetFristOrDefault(p => p.Id == ProductId);

                shoppingCart = new ShoppingCart()
                {
                    ApplicationUserId = claims.Value,
                    ProductId = (int)ProductId,
                    TbProducts = productDiv,
                };

                ShoppingCart CartObj = _unitOfWork.ShippingCart.GetFristOrDefault(
                    u => (u.ApplicationUserId == claims.Value) && (u.ProductId == shoppingCart.ProductId));

                if (CartObj == null)
                {
                    _unitOfWork.ShippingCart.Add(shoppingCart);
                }
                else
                {
                    _unitOfWork.ShippingCart.InCreaseShopingCart(CartObj, shoppingCart.Quantity);
                }
            }

            _unitOfWork.complete();
            return RedirectToAction("Index","Home");
        }
        public IActionResult Cart()
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVm = new ShoppingCartVm()
            {
                LstShoppingCart = _unitOfWork.ShippingCart.GetAll(u => u.ApplicationUserId == claims.Value , word: "TbProducts")
            };
            foreach(var item in shoppingCartVm.LstShoppingCart)
            {
                shoppingCartVm.ShoppingCartTotal += (item.TbProducts.purchaseprices * item.Quantity);
            }

            return View(shoppingCartVm);
        }

        public IActionResult WishListCart(WishlistCart wishlist , int? ProductId)
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);
            wishlist.ApplicationUserId = claims.Value;

            var product = _unitOfWork.Product.GetFristOrDefault(p => p.Id == wishlist.ProductId);
            if (product != null)
            {
                wishlist.Products = product;
                wishlist.ProductId = product.Id;

                WishlistCart WishListObj = _unitOfWork.Wishlist.GetFristOrDefault(
                    u => (u.ApplicationUserId == claims.Value) && (u.ProductId == wishlist.ProductId));

                if (WishListObj == null)
                {
                    _unitOfWork.Wishlist.Add(wishlist);
       //             HttpContext.Session.SetInt32(SolidStatement.SessionKeyWishList,
       //                 _unitOfWork.Wishlist.GetAll(x => x.ApplicationUserId == claims.Value).ToList().Count());
                }
                else
                {

                }
            }
            /* else
             {
                 var productDiv = _unitOfWork.Product.GetFristOrDefault(p => p.Id == id);

                 wishlist = new WishlistCart()
                 {
                     ApplicationUserId = claims.Value,
                     ProductId = (int)id,
                     Products = productDiv,
                 };

                 WishListCart CartObj = _unitOfWork.Wishlist.GetFristOrDefault(
                     u => (u.ApplicationUserId == claims.Value) && (u.ProductId == wishlist.ProductId));

                 if (CartObj == null)
                 {
                     _unitOfWork.ShippingCart.Add(shoppingCart);
                 }
                 else
                 {
                     _unitOfWork.ShippingCart.InCreaseShopingCart(CartObj, shoppingCart.Quantity);
                 }
             }    */

            _unitOfWork.complete();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult WishList(WishlistCart? wishListCart)
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);

            wishListCart = new WishlistCart()
            {
                // Ai : Help
                WishlistItems = _unitOfWork.Wishlist
                                .GetAll(u => u.ApplicationUserId == claims.Value, word: "Products")
                                .Select(w => w.Products)
                                .Where(p => p != null)
                                .ToList()
            };

            foreach(var item in wishListCart.WishlistItems)
            {
                wishListCart.CountWishlist++;
            }

            return View(wishListCart);
        }
        public IActionResult RemoveProductFWislList(int cartId)
        {
            var FWislList = _unitOfWork.Wishlist.GetFristOrDefault(u => u.Products.Id == cartId);
            _unitOfWork.Wishlist.Remove(FWislList);
            _unitOfWork.complete();

            if (FWislList != null)
                return RedirectToAction("WishList");

            return RedirectToAction("Index", "Home");
        }

        // Methods in ShoppingCart Page
        public IActionResult Plus(int cartId)
        {
            var shoppingCart = _unitOfWork.ShippingCart.GetFristOrDefault(u => u.ShoppingCartId == cartId);
            _unitOfWork.ShippingCart.InCreaseShopingCart(shoppingCart, 1);
            _unitOfWork.complete();

            return RedirectToAction("Cart");
        }
        public IActionResult Minus(int cartId)
        {
            var shoppingCart = _unitOfWork.ShippingCart.GetFristOrDefault(u => u.ShoppingCartId == cartId);
            if(shoppingCart.Quantity <= 1)
            {
                _unitOfWork.ShippingCart.Remove(shoppingCart);
            }
            else
            {
                _unitOfWork.ShippingCart.DeCreaseShopingCart(shoppingCart, 1);
            }
            _unitOfWork.complete();

            return RedirectToAction("Cart");
        }
        public IActionResult RemoveProduct(int cartId)
        {
            var shoppingCart = _unitOfWork.ShippingCart.GetFristOrDefault(u => u.ShoppingCartId == cartId);
            shoppingCart.Quantity = 0;
            _unitOfWork.ShippingCart.Remove(shoppingCart);
            _unitOfWork.complete();

            if (shoppingCart != null)
                return RedirectToAction("Cart");

            return RedirectToAction("Index","Home");
        }
        public int NumProInCart()
        {
            int Counter= 0;
            foreach (var item in shoppingCartVm.LstShoppingCart)
            {
                Counter++;
            }
            return Counter;
        }
        // Payment Methods and CheckOut Page
        [HttpGet]
        public IActionResult CheckOut()
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);

            ShoppingCartVm shoppingCart = new ShoppingCartVm()
            {
                LstShoppingCart = _unitOfWork.ShippingCart.GetAll(u=> u.ApplicationUserId == claims.Value , word: "TbProducts"),
                OrderHeader = new()  // Statement ??
            };

            shoppingCart.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUserRepositry.GetFristOrDefault(u => u.Id == claims.Value);

            shoppingCart.OrderHeader.UserName = shoppingCart.OrderHeader.ApplicationUser.UserName;
            shoppingCart.OrderHeader.PhoneNumber = shoppingCart.OrderHeader.ApplicationUser.PhoneNumber;
            shoppingCart.OrderHeader.City = shoppingCart.OrderHeader.ApplicationUser.City;
            shoppingCart.OrderHeader.Adress = shoppingCart.OrderHeader.ApplicationUser.Adress;

            foreach (var item in shoppingCart.LstShoppingCart)
            {
                shoppingCart.ShoppingCartTotal += (item.TbProducts.purchaseprices * item.Quantity);
            }

            return View(shoppingCart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CheckOut(ShoppingCartVm cartVm)
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);

            cartVm.LstShoppingCart = _unitOfWork.ShippingCart.GetAll(u => u.ApplicationUserId == claims.Value, word: "TbProducts");

            cartVm.OrderHeader.OrderDate = DateTime.Now;
            cartVm.OrderHeader.PaymentStatus = SolidStatement.Pending; // Pending, Approved, Proccessing, Cancelled, Shipped, Refund, Rejected
            cartVm.OrderHeader.OrderStatus = SolidStatement.Pending;
            cartVm.OrderHeader.ApplicationUserId = claims.Value;

            foreach (var item in cartVm.LstShoppingCart)
            {
                cartVm.ShoppingCartTotal += (item.TbProducts.purchaseprices * item.Quantity);
                cartVm.OrderHeader.TotalPrice += (item.TbProducts.purchaseprices * item.Quantity);
            }

            if(cartVm.OrderHeader.Carrier == null)
            {
                 cartVm.OrderHeader.Carrier = "Waiting For Carrier"; // Default Value
            }

            _unitOfWork.OrderHeader.Add(cartVm.OrderHeader);
            _unitOfWork.complete();

            // Add Data in Order Details Model
            foreach(var item in cartVm.LstShoppingCart)
            {
                OrderDetails orderDetails = new OrderDetails()
                {
                    ProductId = item.ProductId,
                    OrderHeaderId = cartVm.OrderHeader.Id,
                    Price = item.TbProducts.purchaseprices * item.Quantity,
                    Count = item.Quantity
                };
                _unitOfWork.OrderDetails.Add(orderDetails);
                _unitOfWork.complete();
            }

            // Start Copy & Past From Stripe Documentation
            var domain = "https://localhost:7043" ;
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment",
                // Can Change SuccessUrl and CancelUrl            <--         <---
                SuccessUrl =  domain+$"/User/Order/OrderConfirmation?id={cartVm.OrderHeader.Id}",
                CancelUrl  =  domain+$"/User/Order/Cart"
            };

            foreach(var item in cartVm.LstShoppingCart)
            {
                var sessionLineItemOptions = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.TbProducts.purchaseprices * 100), // Stripe expects amount in cents
                        Currency = "usd", // Change to your currency
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                           // Images = new List<string> { item.TbProducts.Img }, // Product Image URL
                            Name = item.TbProducts.Name,
                        },
                    },
                    Quantity = item.Quantity,
                };
                options.LineItems.Add(sessionLineItemOptions);
            }

            var service = new SessionService();
            Session session = service.Create(options);
            cartVm.OrderHeader.SessionId = session.Id;                       //      <--   Model Data
  //        cartVm.OrderHeader.PaymentInteniD = session.PaymentIntentId;  
            _unitOfWork.complete();

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            // End Copy & Past From Stripe Documentation
        }

        public IActionResult OrderConfirmation(int id , ShoppingCartVm cartVm)
        {
            var UsersClaims = (ClaimsIdentity)User.Identity;
            var claims = UsersClaims.FindFirst(ClaimTypes.NameIdentifier);

            cartVm.LstShoppingCart = _unitOfWork.ShippingCart.GetAll(u => u.ApplicationUserId == claims.Value, word: "TbProducts");
            OrderHeader orderHeader = _unitOfWork.OrderHeader.GetFristOrDefault(u=>u.Id==id);
            cartVm.OrderHeader = orderHeader;

            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);

            string last4 = "";
            if (session.PaymentStatus.ToLower() == "paid")
            {
                var paymentIntentService = new PaymentIntentService();
                var paymentIntent = paymentIntentService.Get(session.PaymentIntentId);

                var paymentMethodService = new PaymentMethodService();
                var paymentMethod = paymentMethodService.Get(paymentIntent.PaymentMethodId);

                // Only show last 4 digits
                last4 = paymentMethod.Card.Last4;

                _unitOfWork.OrderHeader.UpdateOrderStatus(id, SolidStatement.Approve , SolidStatement.Approve);
                orderHeader.PaymentInteniD = session.PaymentIntentId;
                _unitOfWork.complete();
            }

            // Pass last4 to the view
            ViewBag.CardLast4 = last4;

            List<ShoppingCart> shoppingCarts = _unitOfWork.ShippingCart.GetAll(u=> u.ApplicationUserId == orderHeader.ApplicationUserId).ToList();
            if (shoppingCarts.Count == 0)
            {
                shoppingCarts = cartVm.LstShoppingCart.ToList();
            }

            _unitOfWork.ShippingCart.RemoveRange(shoppingCarts);
            _unitOfWork.complete();
            return View(cartVm);
        }

    }
}
