using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.WebUI.Controllers;

public class HomeController : Controller
{
    public async Task<ActionResult> Index()
    {
        return View();
    }

    public async Task<ActionResult> Privacy()
    {
        return View();
    }
}
