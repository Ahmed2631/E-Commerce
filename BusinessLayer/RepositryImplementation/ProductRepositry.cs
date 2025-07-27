using ECommerceDomains.Models;
using ECommerceDomains.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RepositryImplementation
{
    public class ProductRepositry : GenericRepositry<TbPoducts>, IProductRepositry
    {
        private readonly ApplicationDbContext _context;
        public ProductRepositry(ApplicationDbContext context)  : base(context)
        {
            _context = context;
        }
        public void Update(TbPoducts poducts)
        {
           // _context.TbCategories.Update(category);
           var newCategory = _context.TbPoducts.FirstOrDefault(c => c.Id == poducts.Id);
            if (newCategory != null)
            {
                newCategory.Name = poducts.Name;
                newCategory.Salesprices = poducts.Salesprices;
                newCategory.purchaseprices = poducts.purchaseprices;
                newCategory.Description = poducts.Description;
                newCategory.Img = poducts.Img;
                newCategory.Profite = poducts.Salesprices - poducts.purchaseprices;
                //    _context.SaveChanges();
            }
        }
    }
}
