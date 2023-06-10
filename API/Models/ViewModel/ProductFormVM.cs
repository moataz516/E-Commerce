using System.ComponentModel.DataAnnotations.Schema;

namespace E_Website.Models.ViewModel
{
    public class ProductFormVM
    {

        public string categoryId { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int quantity { get; set; }
        public string image { get; set; }
        public string description { get; set; }


        [NotMapped]
        public IFormFile fileImg { get; set; } = null;

    }
}
