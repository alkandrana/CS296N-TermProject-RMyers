using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace CS296N_TermProject_RMyers.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime SignUpDate { get; set; }
        
        [NotMapped]
        public IList<string> RoleNames { get; set; }
    }
}