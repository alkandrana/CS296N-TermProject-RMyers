using System.ComponentModel.DataAnnotations;

namespace CS296N_TermProject_RMyers.Models;

public class Category
{
    [StringLength(2), Required(ErrorMessage = "Please assign a category for organization.")]
    public string CategoryId { get; set; } = string.Empty;
    
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    public virtual ICollection<Article> Articles { get; set; }
}