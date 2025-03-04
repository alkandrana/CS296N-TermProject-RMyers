using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace CS296N_TermProject_RMyers.Data;

public class EntryRepository : IEntryRepository
{
    private AppDbContext _context;

    public EntryRepository(AppDbContext ctx)
    {
        _context = ctx;
    }
    public async Task<List<Article>> GetAllEntriesAsync()
    {
        var entries = await _context.Entries.Include(e => e.Author).Include(
            e => e.Category).ToListAsync();
        return entries;
    }

    /*public List<AppUser> GetAllUsers()
    {
        return _context.AppUsers.ToList();
    }*/

    public async Task<Article?> GetEntryByIdAsync(int id)
    {
        var entry = await _context.Entries.Include(
            e => e.Author).Include(
            e => e.Category)
            .SingleOrDefaultAsync(e => e.ArticleId == id);
        return entry;
    }

    public async Task<Contribution> GetContributionByIdAsync(int id)
    {
        var contribution = await _context.Contributions.Include(
                c => c.Contributor
                )
            .SingleOrDefaultAsync(c => c.ContributionId == id);
        return contribution;
    }

    /*public Category? GetCategoryById(string id)
    {
        var category = _context.Categories.Find(id);
        return category;
    }*/
 
    public async Task<int> StoreEntryAsync(Article model)
    {
        _context.Entries.Add(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> StoreContributionAsync(Contribution model)
    {
        model.Date = DateTime.Now;
        _context.Contributions.Add(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateEntryAsync(Article model)
    {
        _context.Entries.Update(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        List<Category> categories = await _context.Categories.Include(   
            c => c.Entries).OrderByDescending( 
            c => c.Entries.Count).ToListAsync(); 
        
        return categories;
    }
}