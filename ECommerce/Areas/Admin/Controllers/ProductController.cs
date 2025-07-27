using BusinessLayer;
using BusinessLayer.RepositryImplementation;
using ECommerce.Helper;
using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ECommerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = TbRoles.Admin)]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;      // New <<---
        public ProductController(IUnitOfWork unitOfWork , IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment; 
        }

        
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult GetAllProducts()
        {
            var products = _unitOfWork.Product.GetAll(word: "Category"); // Include Category Data
            return Json(new {data= products});
        }
        public IActionResult Craete()
        {
            ProductVM productVM = new ProductVM()
            {
                poducts = new TbPoducts(),
                CagegorySelect = _unitOfWork.Category.GetAll().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Craete(ProductVM ProductVm , IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(ProductVm.poducts);

            string wwwRootPath = _webHostEnvironment.WebRootPath; // Path of wwwroot : Img New
            if(file != null)
            {
                string fileName = Guid.NewGuid().ToString();  // Random Img Name
                var upload = Path.Combine(wwwRootPath, @"Admin\Images\Products");  // Location
                var ext = Path.GetExtension(file.FileName);  // Extension

                using (var fileStreams = new FileStream(Path.Combine(upload,fileName+ext),FileMode.Create))
                {
                                       file.CopyTo(fileStreams);
                }
                ProductVm.poducts.Img = @"\Admin\Images\Products\" + fileName + ext;
            }
            ProductVm.poducts.Profite = ProductVm.poducts.Salesprices - ProductVm.poducts.purchaseprices;

            //context.TbCategories.Add(Product);
            _unitOfWork.Product.Add(ProductVm.poducts);
            //context.SaveChanges();
            _unitOfWork.complete();
            TempData["Create"] = "Product Has Created Successfully";

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound("Error YA Ahmed");
            }
            //     var itemFromDb = context.TbCategories.Find(id);
            //  var itemFromDb = _unitOfWork.Product.GetFristOrDefault(a=>a.Id==id);

            ProductVM productVM = new ProductVM()
            {
                poducts = _unitOfWork.Product.GetFristOrDefault(a => a.Id == id),
                CagegorySelect = _unitOfWork.Category.GetAll().Select(i => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };
            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductVM ProductVm , IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(ProductVm);

            string wwwRootPath = _webHostEnvironment.WebRootPath; // Path of wwwroot : Img New
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();  // Random Img Name
                var upload = Path.Combine(wwwRootPath, @"Admin\Images\Products");  // Location
                var ext = Path.GetExtension(file.FileName);  // Extension

                if(ProductVm.poducts.Img != null)
                {
                    var oldImagePath = Path.Combine(wwwRootPath, ProductVm.poducts.Img.TrimStart('\\'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);     // Delete Old Image
                    }
                }

                using (var fileStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                ProductVm.poducts.Img = @"\Admin\Images\Products\" + fileName + ext;
            }
            ProductVm.poducts.Profite = ProductVm.poducts.Salesprices - ProductVm.poducts.purchaseprices;


            //   context.TbCategories.Update(Product);
            _unitOfWork.Product.Update(ProductVm.poducts);
        //    context.Entry(Product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    context.SaveChanges();
            _unitOfWork.complete();
            TempData["Update"] = "Product Has Updated Successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
      //    var itemFromDb = context.TbCategories.Find(id);
            var itemFromDb = _unitOfWork.Product.GetFristOrDefault(a => a.Id == id);
            return View(itemFromDb);
        }

        public IActionResult DeleteProduct(int? id)
        {
         // var itemFromDb = context.TbCategories.Find(id);
            var itemFromDb = _unitOfWork.Product.GetFristOrDefault(a => a.Id == id);
            if (itemFromDb == null)
            {
                return NotFound();
            }

        //  context.TbCategories.Remove(itemFromDb);
            _unitOfWork.Product.Remove(itemFromDb);
        //    context.SaveChanges();
            _unitOfWork.complete();
            TempData["Delete"] = "Product Has Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}


