using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class TbPoducts
    {
        [ValidateNever]
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50,ErrorMessage ="Max Length is 50 Char")]
        public string Name { get; set; }
        [ValidateNever]
        public string Description { get; set; }
        [ValidateNever]
        public string Img { get; set; }
        [Required]
        [Range(50,99999, ErrorMessage = "Max Price 99999")]
        public decimal Salesprices { get; set; }
        [Required]
        [Range(50, 99999, ErrorMessage = "Max Price 99999")]
        public decimal purchaseprices { get; set; }
        [ValidateNever]
        public decimal Profite { get; set; }

        // Realations
        [Required]
        public int CategoryId { get; set; }
        [ValidateNever]
        public TbCategories Category { get; set; }
    }
}
