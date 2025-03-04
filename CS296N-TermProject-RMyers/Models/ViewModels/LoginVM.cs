using System.ComponentModel.DataAnnotations;

namespace CS296N_TermProject_RMyers.Models.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = "Please enter a username.")]
    [StringLength(50)]
    public string Username { get; set; }
    [Required(ErrorMessage = "Please enter a password.")]
    [StringLength(50)]
    public string Password { get; set; }
    public string ReturnUrl { get; set; }
    public bool RememberMe { get; set; }
    
}