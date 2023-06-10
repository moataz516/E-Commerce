namespace E_Website.Models
{
    public class productImage
    {
        public int Id { get; set; }
        public string productId { get; set; }
        public string image { get; set; }
        public product product { get; set; }
    }
}
