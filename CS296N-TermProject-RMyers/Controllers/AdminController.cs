using CS296N_TermProject_RMyers.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CS296N_TermProject_RMyers.Models;

namespace CS296N_TermProject_RMyers.Controllers;

public class AdminController : Controller
{
    private IEntryRepository _repo;

    public AdminController(IEntryRepository r)
    {
        _repo = r;
    }

// GET
    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Edit(int id)
    {
        List<Category> categories = await _repo.GetAllCategoriesAsync();
        Article? model = await _repo.GetEntryByIdAsync(id);
        ViewBag.Categories = categories;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Article model)
    {
        if (await _repo.UpdateEntryAsync(model) > 0)
        {
            return RedirectToAction("Index");
        }
        ViewBag.Categories = await _repo.GetAllCategoriesAsync();
        return View(model);
    }

    public async Task<IActionResult> Vet(int id)
    {
        var contribution = await _repo.GetContributionByIdAsync(id);
        if (contribution != null)
        {
            Article model = new Article();
            model.Title = contribution.Title;
            model.Content = contribution.Content;
            model.Author = contribution.Contributor;
            return View("Edit", model);
        }
        else
        {
            TempData["error"] = "There was a problem retrieving that contribution." +
                                " Please try again. If the problem persists, " +
                                "contact the Engineering department.";
            return RedirectToAction("Manage");
        }
    }
    
}