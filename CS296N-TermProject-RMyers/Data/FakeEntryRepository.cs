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

    public async Task<List<int>> GetRandomArticlesAsync(int count)
    {
        List<int> ids = Shuffle(_articles.Select(a => a.ArticleId).ToList());
        List<int> selection = count < ids.Count ? ids.GetRange(0, count) : ids;
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
            model.ConversationId = _conversations.Count + 1;
            _conversations.Add(model);
            status = 1;
        }

        return status;
    }

    public async Task<int> DeleteConversationAsync(Conversation model)
    {
        int status = 0;
        if (_conversations[model.ConversationId - 1] == model)
        {
            _conversations.RemoveAt(model.ConversationId - 1);
            status = 1;
        }

        return status;
    }

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

    public async Task<int> UpdateContributionAsync(Contribution model)
    {
        int status = 0;
        if (_contributions[model.ContributionId - 1] == model)
        {
            _contributions[model.ContributionId - 1] = model;
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

    public async Task<List<Article>> GetAllArticlesAsync()
    {
        return _articles;
    }
    
    public List<int> Shuffle(List<int> list)
    {
        Random gen = new Random();
        for (int i = list.Count - 1; i > 0; i--)
        {
            var rand = gen.Next(i + 1);
            (list[rand], list[i]) = (list[i], list[rand]);
        }
        return list;
    }
}