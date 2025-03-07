using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Data.Interfaces;
namespace CS296N_TermProject_RMyers.Data;

public class FakeEntryRepository : IEntryRepository
{
    private List<Article> _entries = new List<Article>();
    private List<Contribution> _contributions = new List<Contribution>();
    private List<AppUser> _users = new List<AppUser>();
    private List<Category> _categories = new List<Category>();
    private List<Conversation> _conversations = new List<Conversation>();

    public async Task<List<Conversation>> GetAllConversationsAsync()
    {
        return _conversations;
    }

    public async Task<List<Contribution>> GetAllContributionsAsync()
    {
        return _contributions;
    }

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        return _entries.Find(r => r.ArticleId == id);
    }

    public async Task<Conversation?> GetConversationByIdAsync(int id)
    {
        return _conversations.Find(r => r.ConversationId == id);
    }

    public async Task<Contribution?> GetContributionByIdAsync(int id)
    {
        return _contributions.Find(r => r.ContributionId == id);
    }

    /*public Category? GetCategoryById(string id)
    {
        return _categories.Find(r => r.CategoryId == id);
    }*/

    public async Task<int> StoreArticleAsync(Article model)
    {
        int status = 0;
        if (model != null && model.Author != null)
        {
            model.ArticleId = _entries.Count + 1;
            _entries.Add(model);
            status = 1;
        }

        return status;
    }

    public async Task<int> StoreContributionAsync(Contribution model)
    {
        int status = 0;
        if (model != null && model.Contributor != null)
        {
            model.ContributionId = _entries.Count + 1;
            _contributions.Add(model);
            status = 1;
        }

        return status;
    }

    public async Task<int> StoreConversationAsync(Conversation model)
    {
        int status = 0;
        if (model != null)
        {
            model.ConversationId = _entries.Count + 1;
            _conversations.Add(model);
            status = 1;
        }

        return status;
    }

    /*public int StoreAppUser(AppUser model)
    {
        int status = 0;
        if (model != null)
        {
            model.AppUserId = _users.Count + 1;
            _users.Add(model);
        }
        return status;
    }
    */

    public async Task<int> UpdateArticleAsync(Article model) //PLACEHOLDER
    {
        int status = 0;
        return status;
    }

    public async Task<int> UpdateConversationAsync(Conversation model)
    {
        int status = 0;
        if (_conversations[model.ConversationId - 1].ConversationId == model.ConversationId)
        {
            _conversations[model.ConversationId - 1] = model;
            status = 1;
        }
        return status;
    }


    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        List<Category> categories = new List<Category>();
        foreach (Article e in _entries)
        {
            categories.Add(e.Category);
        }
        return categories;
    }
    
    /*public List<AppUser> GetAllUsers()
    {
        List<AppUser> users = new List<AppUser>();
        foreach (Article e in _entries)
        {
            users.Add(e.Contributor);
        }
        return users;
    }*/

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return _entries;
    }
    
    
}