using System.ComponentModel.DataAnnotations;
using CS296N_TermProject_RMyers.Models;

namespace CS296N_TermProject_RMyers.Data.Interfaces;

public abstract class Entry
{
    [Required(ErrorMessage = "Please enter a title for ease of reference."), StringLength(100)]
    public abstract string Title { get; set; }
    [Required]
    public abstract string Content { get; set; }
}