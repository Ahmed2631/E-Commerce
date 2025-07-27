using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class WishlistCart
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public IEnumerable<TbPoducts> WishlistItems { get; set; } = new List<TbPoducts>();
        [NotMapped]
        public int CountWishlist { get; set; } = 0;

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public TbPoducts Products { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser applicationUser { get; set; }
    }
}
