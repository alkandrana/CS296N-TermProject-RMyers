using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Data.Interfaces;
namespace CS296N_TermProject_RMyers.Data;

public class FakeEntryRepository : IEntryRepository
{
    private List<Article> _articles = new List<Article>();
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

    public async Task<List<int>> GetRandomArticleIdListAsync(int count)
    {
        List<int> selection = new List<int>();
        if (count < _articles.Count())
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
            selection = _articles.Select(a => a.ArticleId).ToList();
        }
        return selection;
    }

    public async Task<Article?> GetArticleByIdAsync(int id)
    {
        return _articles.Find(r => r.ArticleId == id);
    }

    public async Task<Conversation?> GetConversationByIdAsync(int id)
    {
        return _conversations.Find(r => r.ConversationId == id);
    }

    public async Task<Contribution?> GetContributionByIdAsync(int id)
    {
        return _contributions.Find(r => r.ContributionId == id);
    }
    
    public async Task<int> GetRandomArticleIdAsync()
    {
        Random gen = new Random();
        int max = _articles.Count();
        List<int> ids = _articles.Select(a => a.ArticleId).ToList();
        int randNum = gen.Next(0, max);
        int id = ids[randNum];
        return id;
    }

    public async Task<int> CountArticlesAsync()
    {
        return _articles.Count;
    }

    public async Task<List<int>> GetArticleIdsAsync()
    {
        List<int> ids = new List<int>();
        foreach (Article a in _articles)
        {
            ids.Add(a.ArticleId);
        }

        return ids;
    }

    public async Task<Category?> GetCategoryByIdAsync(string id)
    {
        return _categories.Find(r => r.CategoryId == id);
    }

    public async Task<int> StoreArticleAsync(Article model)
    {
        int status = 0;
        if (model != null && model.Author != null)
        {
            model.ArticleId = _articles.Count + 1;
            _articles.Add(model);
            status = 1;
        }

        return status;
    }

    public async Task<int> StoreContributionAsync(Contribution model)
    {
        int status = 0;
        if (model != null && model.Contributor != null)
        {
            model.ContributionId = _contributions.Count + 1;
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
            model.ConversationId = _articles.Count + 1;
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

    public async Task<int> UpdateArticleAsync(Article model)
    {
        int status = 0;
        int index = model.ArticleId - 1;
        if (_articles[index] == model)
        {
            _articles[index] = model;
            status = 1;
        }
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
        foreach (Article e in _articles)
        {
            categories.Add(e.Category);
        }
        return categories;
    }
    
    /*public List<AppUser> GetAllUsers()
    {
        List<AppUser> users = new List<AppUser>();
        foreach (Article e in _articles)
        {
            users.Add(e.Contributor);
        }
        return users;
    }*/

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return _articles;
    }
    
    
}