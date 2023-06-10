namespace E_Website.Models
{
    public class order
    {
        public string orderId { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public DateTime dateTime { get; set; }
        public string state { get; set; }
        public decimal total { get; set; }
         
        public applicationUser user { get; set; }
       

    }
}
