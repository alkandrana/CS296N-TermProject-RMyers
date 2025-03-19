using CS296N_TermProject_RMyers.Data.Interfaces;
using CS296N_TermProject_RMyers.Models;
using CS296N_TermProject_RMyers.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CS296N_TermProject_RMyers.Controllers;

[Authorize]
public class ConverseController : Controller
{
    private IEntryRepository _repo;
    private UserManager<AppUser> _userManager;

    public ConverseController(IEntryRepository r, UserManager<AppUser> userMngr)
    {
        _repo = r;
        _userManager = userMngr;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    // GET
    public async Task<IActionResult> Portal()
    {
        var model = await _repo.GetAllConversationsAsync();
        ViewBag.CurrentUser = await _userManager.GetUserAsync(User);
        return View(model);
    }

    public IActionResult Contribute()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Contribute(Contribution model)
    {
        if (model != null && ModelState.IsValid)
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
        }
        ModelState.AddModelError("", "There were data entry errors. Please check the form.");
        return View(model);
    }

    public async Task<IActionResult> Start()
    {
        ViewBag.Articles = await _repo.GetAllArticlesAsync();
        ViewBag.Categories = await _repo.GetAllCategoriesAsync();
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Start(Conversation model)
    {
        
        if (model != null && ModelState.IsValid)
        {
            if (model.Author == null)
            {
                model.Author = await _userManager.GetUserAsync(User);
            }
            if (await _repo.StoreConversationAsync(model) > 0)
            {
                var conversations = await _repo.GetAllConversationsAsync();
                return RedirectToAction("Portal", conversations);
            }
            
            ModelState.AddModelError("", "There was a problem saving the conversation. Please try again.");
        }
        else
        {
            ModelState.AddModelError("", "There were data entry errors. Please check the form.");
        }
        ViewBag.Articles = await _repo.GetAllArticlesAsync();
        ViewBag.Categories = await _repo.GetAllCategoriesAsync();
        return View(model);
    }

    public async Task<IActionResult> ConversePage(int id)
    {
        Conversation? model = await _repo.GetConversationByIdAsync(id);
        ViewBag.CurrentUser = await _userManager.GetUserAsync(User);
        return View(model);
    }

    public IActionResult Reply(int id)
    {
        var model = new ResponseVM
        {
            ConversationId = id
        };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Reply(ResponseVM model)
    {
        if (model != null && ModelState.IsValid)
        {
            if (model.Commenter == null)
            {
                model.Commenter = await _userManager.GetUserAsync(User);
            }
            Response reply = new Response
            {
                Content = model.Content,
                Date = DateTime.Now,
                Author = model.Commenter
            };
            var conversation = await _repo.GetConversationByIdAsync(model.ConversationId);
            conversation.Responses.Add(reply);
            await _repo.UpdateConversationAsync(conversation);
            int converseId = conversation.ConversationId;
            return RedirectToAction("ConversePage", new {id = converseId});
        }
        ModelState.AddModelError("", "There were data entry errors. Please check the form.");
        return View(model);
    }

    public async Task<IActionResult> DeleteConversation(int id)
    {
        var conversation = await _repo.GetConversationByIdAsync(id);
        return View("Delete", conversation);
    }
    
    [HttpPost]
    public async Task<IActionResult> ConfirmDeleteConversation(int ConversationId)
    {
        var conversation = await _repo.GetConversationByIdAsync(ConversationId);
        var user = await _userManager.GetUserAsync(User);
        if (user == conversation.Author || User.IsInRole("Admin"))
        {
            if (await _repo.DeleteConversationAsync(conversation) > 0)
            {
                TempData["message"] = "Conversation successfully deleted.";
                TempData["type"] = "success";
            }
            else
            {
                TempData["message"] = "There was a problem deleting the conversation. Please try again.";
                TempData["type"] = "danger";
            }
        }
        else
        {
            TempData["message"] = "There was a problem deleting the conversation. Make sure you have the proper authorization.";
            TempData["type"] = "danger";
        }
        return RedirectToAction("Portal");
    }
}