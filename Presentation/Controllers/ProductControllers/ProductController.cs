using Application.DTOs.ProductDTOs;
using Application.IServices.IProductServices;
using Domain.Entities.ProductEntities;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers.ProductControllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("CreateNewProduct")]
    public async Task<IActionResult> CreateNewProduct([FromForm] CreateProductDTO dto)
    {
        var result = await _productService.CreateAsync(dto);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }
    [HttpGet("GetAllProducts")]
    public async Task<IActionResult> GetAllProducts()
    {
        var result = await _productService.GetAllAsync();
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpGet("GetProductById/{productId}")]
    public async Task<IActionResult> GetProductById(int productId)
    {
        var result = await _productService.GetByIdAsync(productId);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }    
    [HttpDelete("DeleteProduct/{productId}")]
    public async Task<IActionResult> DeleteProduct(int productId)
    {
        var result = await _productService.DeleteAsync(productId);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpGet("SearchByProductName/{productName}")]
    public async Task<IActionResult> SearchByProductName(string productName)
    {
        var result = await _productService.SearchAsync(productName);
        return result.Succeeded ? Ok(result) : NotFound(result);
    }

    [HttpPut("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([FromForm] UpdateProductDTO updateProductDTO)
    {
        var result = await _productService.UpdateAsync(updateProductDTO);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}
