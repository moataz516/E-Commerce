namespace E_Website.Models
{
    public class userCard
    {
        public string userCardId { get; set; }
        public string userId { get; set; }
        public string cardName { get; set; }
        public int cardNumber { get; set; }
        public int expiryMonth { get; set; }
        public int expiryYear { get; set; }
        public string cvv { get; set; }
        public int wallet { get; set; }
        public applicationUser user { get; set; }

    }
}
