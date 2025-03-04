using CS296N_TermProject_RMyers.Data.Interfaces;
using CS296N_TermProject_RMyers.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CS296N_TermProject_RMyers.Controllers;

public class ConverseController : Controller
{
    private IEntryRepository _repo;
    private UserManager<AppUser> _userManager;

    public ConverseController(IEntryRepository r, UserManager<AppUser> userMngr)
    {
        _repo = r;
        _userManager = userMngr;
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Contribute()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contribute(Contribution model)
    {
        if (model.Contributor == null)
        {
            model.Contributor = await _userManager.GetUserAsync(User);
        }

        if (await _repo.StoreContributionAsync(model) > 0)
        {
            return RedirectToAction("Index");
        }
        
        ModelState.AddModelError("", "There was a problem saving the contribution.");
        return View(model);
    }
}