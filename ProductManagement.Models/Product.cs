using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Models
{
    public class Product
    {
        public int id { get; set; }
        [Required]
        public string title { get; set; }
        public string? description { get; set; } = null;
        [Required]
        public decimal price { get; set; } = 0;
        [Required]
        public string category { get; set; }
        [Required]
        public int quantity { get; set; } = 0;
        public string? image { get; set; } = null;
    }
}
