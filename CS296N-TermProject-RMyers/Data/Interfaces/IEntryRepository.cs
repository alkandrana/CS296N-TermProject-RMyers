using CS296N_TermProject_RMyers.Models;
namespace CS296N_TermProject_RMyers.Data.Interfaces;

public interface IEntryRepository
{
    public Task<List<Article>> GetAllArticlesAsync();

    public Task<List<Conversation>> GetAllConversationsAsync();
    
    public Task<List<Contribution>> GetAllContributionsAsync();

    public Task<List<int>> GetRandomArticlesAsync(int count);
    public Task<Article?> GetArticleByIdAsync(int id);
    
    public Task<Conversation?> GetConversationByIdAsync(int id);
    
    public Task<Contribution?> GetContributionByIdAsync(int id);
    
    public Task<int> CountArticlesAsync();
    
    public Task<Category?> GetCategoryByIdAsync(string id);

    public Task<List<int>> GetArticleIdsAsync();
    public Task<int> StoreArticleAsync(Article model);
    
    public Task<int> StoreContributionAsync(Contribution model);
    
    public Task<int> StoreConversationAsync(Conversation model);
    
    public Task<List<Category>> GetAllCategoriesAsync();
    
    public Task<int> UpdateArticleAsync(Article model);
    
    public Task<int> UpdateConversationAsync(Conversation model);
    
    public Task<int> UpdateContributionAsync(Contribution model);
}