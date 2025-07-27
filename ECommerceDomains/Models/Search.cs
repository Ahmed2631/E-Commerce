using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceDomains.Models
{
    public class Search
    {
        public Search()
        {
            Query = string.Empty;
            SearchResults = new List<TbPoducts>();
            SearchResultsExtra = new List<TbPoducts>();
        }

        [Required]
        public string Query { get; set; }
        [ValidateNever]
        public List<TbPoducts>? SearchResults { get; set; }
        [ValidateNever]
        public List<TbPoducts>? SearchResultsExtra { get; set; }
    }
}
