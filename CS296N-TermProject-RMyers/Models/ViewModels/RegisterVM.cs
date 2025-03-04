using System.ComponentModel.DataAnnotations;

namespace CS296N_TermProject_RMyers.Models.ViewModels;

public class RegisterVM
{
    [Required(ErrorMessage = "Please enter a username")]
    [StringLength(50)]
    public string Username { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Please enter a password")]
    [DataType(DataType.Password)]
    [Compare("ConfirmPassword")]
    public string Password { get; set; } = String.Empty;
    
    [Required(ErrorMessage = "Please confirm your password")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string ConfirmPassword { get; set; } = String.Empty;
}