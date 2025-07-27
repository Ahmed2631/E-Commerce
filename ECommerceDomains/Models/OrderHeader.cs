using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }  // <--
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        // Difference ??
        public DateTime  OrderDate    { get; set; }  // <--
        public DateTime  ShippingDate { get; set; }
        public decimal   TotalPrice   { get; set; }
        public string?   OrderStatus  { get; set; }   // <--
        public string?   PaymentStatus { get; set; }  // <--
        public string?   TrackingNum  { get; set; }
        public string? Carrier { get; set; } 
        public DateTime  PaymentDate  { get; set; }

        //  Nessesary for the Online Payment Gateway (Stripe)
        public string?   SessionId    { get; set; }
        public string? PaymentInteniD { get; set; }

        // Data of User : From Form
        [Required(ErrorMessage = "Enter Your Name ,Please")]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Enter Your Adress ,Please")]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string Adress { get; set; }
        [Required(ErrorMessage = "Enter Your City ,Please")]
        [MaxLength(25, ErrorMessage = "Max Length is 25")]
        public string City { get; set; } 
        [Required(ErrorMessage = "Enter Your Phone Number ,Please")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(15, ErrorMessage = "Phone Number must be 11 digits")]
        public string PhoneNumber { get; set; } 
    }
}
