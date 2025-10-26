using Domain.Entities.CategoryEntities;

namespace Domain.Entities.ProductEntities;

public class Product
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public decimal ProductPrice { get; set; }

    public string ProductDescription { get; set; } = string.Empty;

    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public string ImageURL { get; set; }
}
