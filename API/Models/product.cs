using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace E_Website.Models
{
    public class product 
    {
        public string productId { get; set; }
        public string categoryId { get; set; } 
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal? discount { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string createdAt  { get; set; }
        public string updatedAt { get; set; }

        [NotMapped]
        public IFormFile fileImg { get; set; }= null;
        public category category { get; set; }
        public IEnumerable<productImage> productImages { get; set; }






    }
}
