using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.API.Controllers;

[Route("api/v1/products")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
    {
        var products = await _productService.GetAll();
        if (products == null)
            return NotFound("Products not found");

        return Ok(products);
    }

    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult<ProductDTO>> GetById(int id)
    {
        var products = await _productService.GetById(id);
        if (products == null)
            return NotFound("Products not found");

        return Ok(products);
    }

    [HttpPost]
    public async Task<ActionResult> Create(
        [FromBody] ProductDTO productDto
    )
    {
        if (productDto == null)
            return BadRequest("Invalid data");
        await _productService.Create(productDto);

        return new CreatedAtRouteResult(
            "GetProductById",
            new { id = productDto.Id },
            productDto
        );
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(
        int id,
        [FromBody] ProductDTO productDto
    )
    {
        if (productDto == null)
            return BadRequest("Invalid data");

        productDto.Id = id;
        await _productService.Update(productDto);

        return Ok(productDto);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Remove(int id)
    {
        var products = await _productService.GetById(id);
        if (products == null)
            return NotFound("Products not found");

        await _productService.Remove(id);
        return Ok(products);
    }
}