using System.ComponentModel.DataAnnotations;
using CS296N_TermProject_RMyers.Data.Interfaces;

namespace CS296N_TermProject_RMyers.Models;

public class Contribution : Entry
{
    public int ContributionId { get; set; }
    
    [Required(ErrorMessage = "Please enter a title for ease of reference."), StringLength(100)]
    public override string Title { get; set; } = "";

    [Required] public override string Content { get; set; } = "";
    
    public DateTime Date { get; set; }
    
    public AppUser? Contributor { get; set; }
    
}