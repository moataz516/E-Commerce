namespace E_Website.Models
{
    public class productCategory
    {
        public int productId { get; set; }
        public int categoryId { get; set; }
        public IEnumerable<category> category { get; set; }
        public IEnumerable<product> product { get; set; }
    }
}
