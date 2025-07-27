using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class CategoeyRepositry: GenericRepositry<TbCategories>, ICategoryRepositry
    {
        private readonly ApplicationDbContext _context;
        public CategoeyRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }
        public void Update(TbCategories category)
        {
           // _context.TbCategories.Update(category);
           var newCategory = _context.TbCategories.FirstOrDefault(c => c.Id == category.Id);
            if (newCategory != null)
            {
                newCategory.Name = category.Name;
                newCategory.CategoryFactor = category.CategoryFactor;
                newCategory.Description = category.Description;
                newCategory.UpdatedAt = DateTime.Now;
          //    _context.SaveChanges();
            }
        }
    }
}
