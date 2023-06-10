namespace E_Website.Models.ViewModel
{
    public class orderDetailsVM
    {
        public string orderId { get; set; }
        public string productId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public decimal total { get; set; }
    }
}
