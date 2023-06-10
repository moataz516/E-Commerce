using Microsoft.AspNetCore.Identity;

namespace E_Website.Models
{
    public class applicationUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public userCard userCard { get; set; }
        public ICollection<order> orders { get; set; }

    }
}
