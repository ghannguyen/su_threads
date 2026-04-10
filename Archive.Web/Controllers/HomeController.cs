using System.Diagnostics;
using Archive.Web.Extensions;
using Archive.Web.Models;
using Archive.Web.Services;
using Archive.Web.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Archive.Web.Controllers;

public class HomeController : Controller
{
    private readonly IFeedService _feedService;

    public HomeController(IFeedService feedService)
    {
        _feedService = feedService;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity?.IsAuthenticated == true)
        {
            return RedirectToAction("Index", "Feed");
        }

        var model = await _feedService.GetLandingPageAsync(User.GetUserId());
        return View(model);
    }

    public IActionResult About()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }

    [ActionName("StatusCode")]
    public IActionResult StatusCodePage(int code)
    {
        ViewBag.StatusCode = code;
        return View("StatusCode");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
