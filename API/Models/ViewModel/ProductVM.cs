using System.ComponentModel.DataAnnotations.Schema;

namespace E_Website.Models.ViewModel
{
    public class ProductVM
    {
        public string productId { get; set; }
        public string categoryId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public decimal discount { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
    }
}
