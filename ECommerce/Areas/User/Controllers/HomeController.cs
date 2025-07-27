using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using Microsoft.AspNetCore.Mvc;
using ECommerceDomains.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using X.PagedList;

namespace ECommerce.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
                    _unitOfWork = unitOfWork;
        }
        public IActionResult Index( VwHomePage vwHomePage)
        {
            vwHomePage.FeaturedProducts = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                         Take(8).ToList();

            vwHomePage.RecentProducts = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                         Take(8).ToList();

            vwHomePage.CategoryList = _unitOfWork.Category.GetAll().ToList();
            return View(vwHomePage);
        }
        public IActionResult SpecialOffer1(int categoryId)
        {
            VwHomePage vwHomePage = new VwHomePage();
            vwHomePage.FeaturedProducts = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                         Where(a=>a.CategoryId==2 || a.CategoryId==18).Take(20).ToList();
            return View("Shop", vwHomePage);
        }
        public IActionResult SpecialOffer2(int categoryId)
        {
            VwHomePage vwHomePage = new VwHomePage();
            vwHomePage.FeaturedProducts = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                         Where(a => a.CategoryId == 15 || a.CategoryId == 9).Take(20).ToList();
            return View("Shop", vwHomePage);
        }
        public IActionResult Shop(int categoryId , int ? page)
        {
            // For Pagination
        //    var PageNumber = page ?? 1; // Default Page Number
        //    int PageSize = 12;          // Number of items per page

            VwHomePage vwHomePage = new VwHomePage();
            vwHomePage.FeaturedProducts = (List<TbPoducts>?) _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                    Where(a=>a.CategoryId== categoryId).Take(12).ToList();
            if(categoryId==0)
            {
                vwHomePage.FeaturedProducts = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                          Take(20).ToList();
            }
            return View(vwHomePage);
        }
        public IActionResult ProductDetails(int productId , int categoryId)
        {
            ShoppingCart shoppingCart = new ShoppingCart()
            {
                TbProducts = _unitOfWork.Product.GetFristOrDefault(a => a.Id == productId, word: "Category"),
                Quantity = 1
            };

            shoppingCart.RelatedProduct = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                             Where(a => a.CategoryId == categoryId).Take(4).ToList();

            return View(shoppingCart);
        }

        [HttpPost]
        public IActionResult Search(string query)  // MacBook Pro
        {
            Search search = new Search();

            if (query == null)
            {
                search.SearchResults = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                        Take(4).ToList();
                search.SearchResultsExtra = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                        Take(4).ToList();
                return View(search);
            }
                
            search.Query = query.Trim();

            search.SearchResults = _unitOfWork.Product.GetAll().Where(a => a.Name.Contains(search.Query, StringComparison.OrdinalIgnoreCase) ||
                                  a.Name.Equals(search.Query)).ToList();

            search.SearchResultsExtra = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).Take(4).ToList();

            if (search.SearchResults.Count == 0)
            {
                search.SearchResults = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                       Take(2).ToList();
                search.SearchResultsExtra = _unitOfWork.Product.GetAll().OrderBy(x => Guid.NewGuid()).
                                       Take(4).ToList();
            }

            return View(search);
        }


        public IActionResult ContactUs()
        {
            return View();
        }

        // Feature
        public IActionResult Filter()
        {
            return View();
        }
        
    }
}
