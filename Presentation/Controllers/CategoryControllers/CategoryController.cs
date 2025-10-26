using Application.IServices.ICategoryServices;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.CategoryControllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet("GetCategoryList")]
    public async Task<IActionResult> GetAllCategory()
    {
        var result = await _categoryService.GetCategoryListAsync();
        return result.Succeeded ? Ok(result) : NotFound(result);
    } 
    
    [HttpPost("Create")]
    public async Task<IActionResult> Create(string categoryName)
    {
        var result = await _categoryService.CreateAsync(categoryName);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }
}
