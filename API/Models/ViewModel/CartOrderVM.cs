namespace E_Website.Models.ViewModel
{
    public class CartOrderVM
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string productId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int qty { get; set; }
        public decimal total { get; set; }

    }
}
