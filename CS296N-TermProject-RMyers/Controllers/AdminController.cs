using CS296N_TermProject_RMyers.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CS296N_TermProject_RMyers.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private IEntryRepository _repo;
    private UserManager<AppUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;

    public AdminController(IEntryRepository r, UserManager<AppUser> userMngr, RoleManager<IdentityRole> roleMngr)
    {
        _repo = r;
        _userManager = userMngr;
        _roleManager = roleMngr;
    }


    public IActionResult Index()
    {
        return View();
    }
    public async Task<IActionResult> Manage()
    {
        List<AppUser> users = new List<AppUser>();
        foreach (AppUser user in (_userManager.Users.ToList()))
        {
            user.RoleNames = await _userManager.GetRolesAsync(user);
            users.Add(user);
        }

        UserVM model = new UserVM
        {
            Users = users,
            Roles = _roleManager.Roles.ToList()
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUser(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                string errorMessage = "";
                foreach (IdentityError error in result.Errors)
                {
                    errorMessage += error.Description + " | ";
                }

                TempData["message"] = errorMessage;
            }
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddToAdmin(string id)
    {
        IdentityRole adminRole = await _roleManager.FindByNameAsync("Admin");
        if (adminRole == null)
        {
            TempData["message"] = "Admin role does not exist." +
                                  "Click 'Create Admin Role' button to create it.";
        }
        else
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            await _userManager.AddToRoleAsync(user, adminRole.Name);
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> RemoveFromAdmin(string id)
    {
        AppUser user = await _userManager.FindByIdAsync(id);
        var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
        if (result.Succeeded)
        {
            TempData["message"] = user.UserName + "has been successfully removed from the admin role.";
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdminRole()
    {
        var result = await _roleManager.CreateAsync(new IdentityRole("Admin"));
        if (result.Succeeded)
        {
            TempData["message"] = "Admin role has been successfully created.";
        }
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteRole(string id)
    {
        IdentityRole role = await _roleManager.FindByIdAsync(id);
        var result = await _roleManager.DeleteAsync(role);
        if (result.Succeeded)
        {
            TempData["message"] = role.Name + " role has been successfully deleted.";
        }
        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Edit(int id)
    {
        List<Category> categories = await _repo.GetAllCategoriesAsync();
        Article? model = await _repo.GetArticleByIdAsync(id);
        ViewBag.Categories = categories;
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Article model)
    {
        if (ModelState.IsValid)
        {
            if (await _repo.UpdateArticleAsync(model) > 0)
            {
                // TODO: change Contribution status to not active
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "There was a problem updating the article.");
        }
        ModelState.AddModelError("", "There were data entry errors. Please check the form.");
        ViewBag.Categories = await _repo.GetAllCategoriesAsync();
        return View(model);
    }

    public async Task<IActionResult> Select()
    {
        List<Article> model = await _repo.GetAllArticlesAsync();
        return View(model);
    }

    public async Task<IActionResult> Vet()
    {
        List<Contribution> model = await _repo.GetAllContributionsAsync();
        return View(model);
    }
    public async Task<IActionResult> EditContribution(int id)
    {
        var contribution = await _repo.GetContributionByIdAsync(id);
        if (contribution != null)
        {
            Article model = new Article
            {
                Title = contribution.Title,
                Content = contribution.Content,
                Author = contribution.Contributor
            };
            ViewBag.Categories = await _repo.GetAllCategoriesAsync();
            return View("Edit", model);
        }
        else
        {
            if (TempData != null)
            {
                TempData["error"] = "There was a problem retrieving that contribution." +
                                    " Please try again. If the problem persists, " +
                                    "contact the Engineering department.";
            }
            return View("Vet");
        }
    }
    
}