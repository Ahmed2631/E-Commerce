using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class VwHomePage
    {
        public List<TbPoducts>? FeaturedProducts { get; set; } = new List<TbPoducts>();
        public List<TbPoducts>? RecentProducts { get; set; } = new List<TbPoducts>();
        public List<TbCategories>? CategoryList { get; set; } = new List<TbCategories>();
    }
}
