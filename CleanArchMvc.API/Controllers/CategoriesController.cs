using Microsoft.AspNetCore.Mvc;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.DTOs;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAll();
            if (categories == null)
                return NotFound("Categories not found");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetById(id);
            if (category == null)
                return NotFound("Category not found");

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> Create(
            [FromBody] CategoryDTO categoryDto
        )
        {
            if (categoryDto == null)
                return BadRequest("Invalid data");

            await _categoryService.Create(categoryDto);

            return new CreatedAtRouteResult(
                "GetById",
                new { id = categoryDto.Id },
                categoryDto
            );
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(
            int id,
            [FromBody] CategoryDTO categoryDto
        )
        {
            if (id == null)
                return BadRequest("Invalid id");
            if (categoryDto == null)
                return BadRequest("Invalid data");

            categoryDto.Id = id;

            await _categoryService.Update(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Remove(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
                return NotFound("Category not found");

            await _categoryService.Remove(id);

            return Ok(category);
        }
    }
}