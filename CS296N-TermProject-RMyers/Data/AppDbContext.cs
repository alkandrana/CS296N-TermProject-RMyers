using CS296N_TermProject_RMyers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CS296N_TermProject_RMyers.Data;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    
    public DbSet<Article> Articles { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<Contribution> Contributions { get; set; }
    
    public DbSet<Conversation> Conversations { get; set; }
    
}