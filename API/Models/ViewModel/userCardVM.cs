using Microsoft.Build.Framework;

namespace E_Website.Models.ViewModel
{
    public class userCardVM

    {

        
        public string userId { get; set; }
        [Required]
        public string cardName { get; set; }
        [Required]
        public int cardNumber { get; set; }
        [Required]

        public int expiryMonth { get; set; }
        [Required]

        public int expiryYear { get; set; }
        [Required]

        public string cvv { get; set; }
    }
}
