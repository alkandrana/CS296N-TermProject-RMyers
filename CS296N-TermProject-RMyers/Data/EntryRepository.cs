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
    public async Task<List<Article>> GetAllArticlesAsync()
    {
        var entries = await _context.Articles.Include(e => e.Author).Include(
            e => e.Category).ToListAsync();
        return entries;
    }

    public async Task<List<Conversation>> GetAllConversationsAsync()
    {
        var conversations = await _context.Conversations.Include(
                c => c.Category)
            .Include(c => c.Responses)
            .Include(c => c.Author)
            .Include(c => c.Article)
            .ToListAsync();
        return conversations;
    }

    public async Task<List<Contribution>> GetAllContributionsAsync()
    {
        var contributions = await _context.Contributions
            .Include(c => c.Contributor)
            .ToListAsync();
        return contributions;
    }

    /*public List<AppUser> GetAllUsers()
    {
        return _context.AppUsers.ToList();
    }*/

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        var article = await _context.Articles.Include(
            e => e.Author).Include(
            e => e.Category)
            .SingleOrDefaultAsync(e => e.ArticleId == id);
        return article;
    }

    public async Task<Conversation?> GetConversationByIdAsync(int id)
    {
        var conversation = await _context.Conversations
            .Include(c => c.Category)
            .Include(c => c.Responses.OrderBy(r => r.Date))
            .Include(c => c.Author)
            .Include(c => c.Article)
            .SingleOrDefaultAsync(c => c.ConversationId == id);
        return conversation;
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
 
    public async Task<int> StoreArticleAsync(Article model)
    {
        _context.Articles.Add(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> StoreContributionAsync(Contribution model)
    {
        model.Date = DateTime.Now;
        _context.Contributions.Add(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> StoreConversationAsync(Conversation model)
    {
        model.StartDate = DateTime.Now;
        _context.Conversations.Add(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateArticleAsync(Article model)
    {
        _context.Articles.Update(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateConversationAsync(Conversation model)
    {
        _context.Conversations.Update(model);
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