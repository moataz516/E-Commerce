using System.ComponentModel.DataAnnotations.Schema;

namespace E_Website.Models.ViewModel
{
    public class ProductWithImage
    {
        public IEnumerable<product> products { get; set; }
        public byte[] imageData { get; set; }
    }
}
