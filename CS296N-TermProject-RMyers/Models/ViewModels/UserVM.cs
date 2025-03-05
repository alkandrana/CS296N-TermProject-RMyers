using Microsoft.AspNetCore.Identity;
namespace CS296N_TermProject_RMyers.Models.ViewModels;

public class UserVM
{
    public IEnumerable<AppUser> Users { get; set; }
    public IEnumerable<IdentityRole> Roles { get; set; }
}