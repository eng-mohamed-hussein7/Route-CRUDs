using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ProductDTOs;

public class UpdateProductDTO
{
    [Required]
    public int Id { get; set; }
    // Product name: only letters (Arabic or English) and spaces, 2–50 chars
    [RegularExpression(@"^[A-Z][a-z]{3,8}$", ErrorMessage = "Product name must contain only letters and spaces (2–50 characters).")]
    public string ProductName { get; set; }

    // Price: positive number, up to 2 decimal places
    [RegularExpression(@"^[1-9][0-9][0-9]\.[0-9]{,2})?$", ErrorMessage = "Product price must be a positive number with up to two decimal places.")]
    public decimal ProductPrice { get; set; }

    // Description: optional, up to 200 characters
    [RegularExpression(@"^.{3,200}$", ErrorMessage = "Product description cannot exceed 200 characters.")]
    public string ProductDescription { get; set; } = string.Empty;

    // Category: positive integer (1–9999)
    [RegularExpression(@"^[1-9][0-9]{0,3}$", ErrorMessage = "Product category must be a positive integer.")]
    public int ProductCategoryId { get; set; }

    public IFormFile Image { get; set; }
}
