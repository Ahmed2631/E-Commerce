using Microsoft.AspNetCore.Mvc;
using ECommerceDomains.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BusinessLayer;
using ECommerceDomains.Repositories;
using BusinessLayer.RepositryImplementation;
using Microsoft.AspNetCore.Authorization;
using ECommerce.Helper;

namespace ECommerce.Areas.Admin.Controllers
{
    //[Authorize(Roles = TbRoles.Admin)]
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CatogaryController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CatogaryController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult List()
        {
            var item = _unitOfWork.Category.GetAll();
            return View(item);
        }
        public IActionResult Craete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Craete(TbCategories category , IFormFile file)
        {
            if (!ModelState.IsValid)
                return View(category);

            string wwwRootPath = _webHostEnvironment.WebRootPath; // Path of wwwroot : Img New
            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString();  // Random Img Name
                var upload = Path.Combine(wwwRootPath, @"Admin\Images\Category");  // Location
                var ext = Path.GetExtension(file.FileName);  // Extension

                using (var fileStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                {
                    file.CopyTo(fileStreams);
                }
                category.CategoryImg = @"\Admin\Images\Category\" + fileName + ext;
            }

            //context.TbCategories.Add(category);
            _unitOfWork.Category.Add(category);
            //context.SaveChanges();
            _unitOfWork.complete();
            TempData["Create"] = "Category Has Created Successfully";

            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();
            }
            //     var itemFromDb = context.TbCategories.Find(id);
            var itemFromDb = _unitOfWork.Category.GetFristOrDefault(a=>a.Id==id);
            return View(itemFromDb);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TbCategories category, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View(category);


                string wwwRootPath = _webHostEnvironment.WebRootPath; // Path of wwwroot : Img New
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();  // Random Img Name
                    var upload = Path.Combine(wwwRootPath, @"Admin\Images\Category");  // Location
                    var ext = Path.GetExtension(file.FileName);  // Extension

                    if (category.CategoryImg != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, category.CategoryImg.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);     // Delete Old Image
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(upload, fileName + ext), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    category.CategoryImg = @"\Admin\Images\Category\" + fileName + ext;
                }   

            //   context.TbCategories.Update(category);
            _unitOfWork.Category.Update(category);
        //    context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        //    context.SaveChanges();
            _unitOfWork.complete();
            TempData["Update"] = "Category Has Updated Successfully";

            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
      //    var itemFromDb = context.TbCategories.Find(id);
            var itemFromDb = _unitOfWork.Category.GetFristOrDefault(a => a.Id == id);
            return View(itemFromDb);

        }

        public IActionResult DeleteCategory(int? id)
        {
         // var itemFromDb = context.TbCategories.Find(id);
            var itemFromDb = _unitOfWork.Category.GetFristOrDefault(a => a.Id == id);
            if (itemFromDb == null)
            {
                return NotFound();
            }

        //  context.TbCategories.Remove(itemFromDb);
            _unitOfWork.Category.Remove(itemFromDb);
        //    context.SaveChanges();
            _unitOfWork.complete();
            TempData["Delete"] = "Category Has Deleted Successfully";
            return RedirectToAction("List");
        }
    }
}


