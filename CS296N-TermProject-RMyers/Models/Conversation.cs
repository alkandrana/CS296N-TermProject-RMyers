using CS296N_TermProject_RMyers.Data.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CS296N_TermProject_RMyers.Models;

public class Conversation
{
    public int ConversationId { get; set; }

    [Required, StringLength(255)] public string Title { get; set; } = "";
    
    [Required]
    public string Content { get; set; } = "";
    
    [StringLength(2)]
    public string CategoryId { get; set; } = "G";
    public Category? Category { get; set; }
    
    public DateTime StartDate { get; set; }
    
    [Display(Name = "Article")]
    public int? ArticleId { get; set; }
    public Article? Article { get; set; }
    
    public AppUser? Author { get; set; }

    public ICollection<Response>? Responses { get; set; } = new List<Response>();
    
    // Format title as a title for display in the view
    public string FormatTitle()
    {
        string[] titleWords = Title.Split(" ");
        for (int i = 0; i < titleWords.Length; i++)
        {
            char first = titleWords[i][0];
            titleWords[i] = Char.ToUpper(first) + titleWords[i].Substring(1);
        }
        return string.Join(" ", titleWords);
    }

}