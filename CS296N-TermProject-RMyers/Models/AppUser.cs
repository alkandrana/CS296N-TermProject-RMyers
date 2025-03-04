using Microsoft.AspNetCore.Identity;

namespace CS296N_TermProject_RMyers.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime SignUpDate { get; set; }
    }
}