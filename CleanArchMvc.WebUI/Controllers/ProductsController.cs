using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.WebUI.Controllers;

public class ProductsController : Controller
{
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;
    private readonly IWebHostEnvironment _environment;

    public ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IWebHostEnvironment environment
    )
    {
        _productService = productService;
        _categoryService = categoryService;
        _environment = environment;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAll();
        return View(products);
    }

    // TODO: To solve the create product bug.
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        ViewBag.CategoryId =
            new SelectList(await _categoryService.GetAll(), "Id", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.Create(productDTO);
            return RedirectToAction(nameof(Index));
        }
        return View(productDTO);
    }

    // TODO: To solve the edit product bug.
    [HttpGet]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var productDTO = await _productService.GetById(id);

        if (productDTO == null) return NotFound();
        var categories = await _categoryService.GetAll();

        ViewBag.CategoryId =
            new SelectList(categories, "Id", "Name", productDTO.CategoryId);
        return View(productDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductDTO productDTO)
    {
        if (ModelState.IsValid)
        {
            await _productService.Update(productDTO);
            return RedirectToAction(nameof(Index));
        }
        return View(productDTO);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var productDTO = await _productService.GetById(id);

        if (productDTO == null) return NotFound();
        return View(productDTO);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        await _productService.Remove(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var productDTO = await _productService.GetById(id);

        if (productDTO == null) return NotFound();
        var wwwroot = _environment.WebRootPath;
        var image = Path.Combine(wwwroot, "images\\" + productDTO.Image);
        var exists = System.IO.File.Exists(image);
        ViewBag.ImageExist = exists;

        return View(productDTO);
    }
}
