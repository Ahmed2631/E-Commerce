using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ECommerceDomains.Models
{
    public class TbCategories
    {
        public TbCategories()
        {
            Name = string.Empty;
            CategoryFactor = string.Empty;
        }

        [Key]
        [ValidateNever]
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(50, ErrorMessage = "Category Name cannot exceed 100 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Category Name is required")]
        [MaxLength(50, ErrorMessage = "Category Name cannot exceed 100 characters")]
        public string CategoryFactor { get; set; }  
        [ValidateNever]
        public string CategoryImg { get; set; }
        public string? Description { get; set; } 
        public DateTime? CreatedAt { get; set; } 
        public DateTime? UpdatedAt { get; set; } 
    }
}
