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

    public async Task<List<int>> GetRandomArticleIdListAsync(int count)
    {
        List<int> selection = new List<int>();
        if (count < _context.Articles.Count())
        {
            int id = await GetRandomArticleIdAsync();
            for (int i = 0; i < count; i++)
            {
                while (selection.Contains(id))
                {
                    id = await GetRandomArticleIdAsync();
                }
                selection.Add(id);
            }
        }
        else
        {
            selection = await _context.Articles.Select(a => a.ArticleId).ToListAsync();
        }

        return selection;
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
            .Include(c => c.Responses.OrderBy(r => r.Date)).ThenInclude(r => r.Author)
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

    public async Task<int> GetRandomArticleIdAsync()
    {
        Random gen = new Random();
        int max = _context.Articles.Count();
        List<int> ids = await _context.Articles.Select(a => a.ArticleId).ToListAsync();
        int randNum = gen.Next(0, max);
        int id = ids[randNum];
        return id;
    }

    public async Task<int> CountArticlesAsync()
    {
        return await _context.Articles.CountAsync();
    }

    public async Task<List<int>> GetArticleIdsAsync()
    {
        return await _context.Articles.Select(a => a.ArticleId).ToListAsync();
    }
    
    public async Task<Category?> GetCategoryByIdAsync(string id)
    {
        var category = await _context.Categories.FindAsync(id);
        return category;
    }
 
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

    public async Task<int> UpdateContributionAsync(Contribution model)
    {
        _context.Contributions.Update(model);
        return await _context.SaveChangesAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        List<Category> categories = await _context.Categories.Include(   
            c => c.Articles).OrderByDescending( 
            c => c.Articles.Count).ToListAsync(); 
        
        return categories;
    }
}