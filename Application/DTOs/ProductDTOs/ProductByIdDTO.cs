namespace Application.DTOs.ProductDTOs;

public class ProductByIdDTO
{
    public int Id { get; set; }
    public string ProductName { get; set; }
    public decimal ProductPrice { get; set; }
    public string ProductDescription { get; set; } = string.Empty;
    public int ProductCategory { get; set; }
    public string ImageURL { get; set; }
}
