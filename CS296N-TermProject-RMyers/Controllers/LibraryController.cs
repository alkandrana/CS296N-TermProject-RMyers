using CS296N_TermProject_RMyers.Data.Interfaces;
using CS296N_TermProject_RMyers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace CS296N_TermProject_RMyers.Controllers;

[Authorize]
public class LibraryController : Controller
{
    private IEntryRepository _repo;
    private UserManager<AppUser>? _userManager;

    public LibraryController(IEntryRepository r, UserManager<AppUser>? userMngr)
    {
        _repo = r;
        _userManager = userMngr;
    }
    
    public async Task<IActionResult> Index()
    {
        // display a featured article on the home page
        Random gen = new Random();
        List<Article> entries = await _repo.GetAllArticlesAsync();
        // set variables random selection
        int max = entries.Count;
        int id = gen.Next(1, max);
        // send random selection to the view
        Article model = entries[id];
        return View(model);
    }

    public async Task<IActionResult> Browse()
    {
        //TODO: simplify this repo method
        // load the articles according to their category
        var categories = await _repo.GetAllCategoriesAsync();
        return View(categories);
    }

    public async Task<IActionResult> Search(string key)
    {
        List<Article> entries = await _repo.GetAllArticlesAsync();
        // TODO: 1. return list of search results 2. search both title and contents
        Article? model = entries.FirstOrDefault(
            e => e.Title.ToLower().Contains(key.ToLower()));
        return View("Reader", model);
    }

    public async Task<IActionResult> SearchById(int id)
    {
        Article? model = await _repo.GetArticleByIdAsync(id);
        return View("Reader", model);
    }
    
    
}