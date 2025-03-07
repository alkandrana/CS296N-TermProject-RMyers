using System.ComponentModel.DataAnnotations;
using CS296N_TermProject_RMyers.Data.Interfaces;

namespace CS296N_TermProject_RMyers.Models;

public class Response
{
    public int ResponseId { get; set; }
    [Required] public string Content { get; set; } = "";
    public DateTime Date { get; set; }
    
    public Conversation Conversation { get; set; }
    public AppUser? Author { get; set; }
}