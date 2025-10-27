using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Application.DTOs.ProductDTOs;

public class UpdateProductDTO
{
    [Required]
    public int Id { get; set; }
    // ✅ Product name: English letters, numbers, spaces, 2–50 characters
    [RegularExpression(@"^[A-Za-z0-9\s]{2,50}$",
    ErrorMessage = "Product name must contain only English letters, numbers, and spaces (2–50 characters).")]
    public string ProductName { get; set; }

    // ✅ Product price: positive number, up to 2 decimal places (1 – 9,999,999.99)
    [RegularExpression(@"^[1-9]\d{0,6}(\.\d{1,2})?$",
    ErrorMessage = "Product price must be a positive number with up to two decimal places (max 9,999,999.99).")]
    public decimal ProductPrice { get; set; }

    // ✅ Description: English letters, numbers, punctuation, 3–200 characters
    [RegularExpression(@"^[A-Za-z0-9\s.,!?'"":;()\-\r\n]{3,200}$",
    ErrorMessage = "Product description must be between 3 and 200 English characters.")]
    public string ProductDescription { get; set; } = string.Empty;

    // ✅ Category: positive integer (1–9999)
    [RegularExpression(@"^[1-9][0-9]{0,3}$",
    ErrorMessage = "Product category must be a positive integer between 1 and 9999.")]
    public int ProductCategoryId { get; set; }

    // ✅ Image file (optional validation)
    public IFormFile? Image { get; set; }
}
