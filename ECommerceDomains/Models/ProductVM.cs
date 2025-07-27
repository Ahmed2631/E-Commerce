using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class ProductVM
    {
        public TbPoducts poducts { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CagegorySelect { get; set; }
    }
}
