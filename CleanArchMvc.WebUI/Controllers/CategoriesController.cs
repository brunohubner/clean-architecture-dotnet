using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Interfaces;

namespace CleanArchMvc.WebUI.Controllers;

public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var categories = await _categoryService.GetAll();
        return View(categories);
    }
}
