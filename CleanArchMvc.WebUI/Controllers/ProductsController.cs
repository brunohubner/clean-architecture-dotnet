using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Interfaces;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();
        return View(products);
    }
}
