﻿namespace E_Website.Models
{
    public class orderProduct
    {
        public string Id { get; set; }
        public string orderId { get; set; }
        public string productId { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public decimal total { get; set; }
        public ICollection<order> order { get; set; }
        public ICollection<product> product { get; set; }
    }
}
