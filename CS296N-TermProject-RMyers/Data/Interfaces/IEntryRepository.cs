using CS296N_TermProject_RMyers.Models;
namespace CS296N_TermProject_RMyers.Data.Interfaces;

public interface IEntryRepository
{
    public Task<List<Article>> GetAllEntriesAsync();
    
    public Task<Article?> GetEntryByIdAsync(int id);
    
    public Task<Contribution> GetContributionByIdAsync(int id);
    // public Category? GetCategoryById(string id);

    public Task<int> StoreEntryAsync(Article model);
    
    public Task<int> StoreContributionAsync(Contribution model);
    
    public Task<List<Category>> GetAllCategoriesAsync();
    
    public Task<int> UpdateEntryAsync(Article model);
}