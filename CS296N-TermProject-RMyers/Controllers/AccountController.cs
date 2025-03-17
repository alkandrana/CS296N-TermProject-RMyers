using CS296N_TermProject_RMyers.Data;
using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CS296N_TermProject_RMyers.Controllers;

public class AccountController : Controller
{
    private UserManager<AppUser> _userManager;
    private SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userMngr, SignInManager<AppUser> signInMngr)
    {
        _userManager = userMngr;
        _signInManager = signInMngr;
    }
    public IActionResult Login(string returnUrl = "")
    {
        LoginVM model = new LoginVM{ReturnUrl = returnUrl};
        return View(model);
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginVM model)
    {
        var result = await _signInManager.PasswordSignInAsync(
            model.Username, model.Password, isPersistent: model.RememberMe, 
            lockoutOnFailure: false);
        if (result.Succeeded)
        {
            if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        ModelState.AddModelError("", "Invalid username/password.");
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterVM model)
    {
        if (ModelState.IsValid)
        {
            var user = new AppUser
            {
                UserName = model.Username,
                SignUpDate = DateTime.Now
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        }
        return View(model);
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}