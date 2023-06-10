using Microsoft.Build.Framework;

namespace E_Website.Models.ViewModel
{
    public class RegisterVM
    {
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string userName => $"{firstName.ToLower()}_{lastName.ToLower()}";
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string phone { get; set; }
    }
}
