using System.ComponentModel.DataAnnotations;
using CS296N_TermProject_RMyers.Data.Interfaces;

namespace CS296N_TermProject_RMyers.Models
{
    public class Article : Entry
    {
        public int ArticleId { get; set; }

        [StringLength(100), Required(ErrorMessage = "Please enter a title for ease of reference.")]
        public override string Title { get; set; } = "";

        public override string Content { get; set; } = "";

        public AppUser? Author { get; set; }

        [StringLength(2), Required(ErrorMessage = "Please assign a category for organization.")]
        public string CategoryId { get; set; } = "G";

        public Category? Category { get; set; }

        public bool Protected { get; set; }
    }
    
}