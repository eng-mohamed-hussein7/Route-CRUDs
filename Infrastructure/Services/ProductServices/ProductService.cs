using Application.DTOs.ProductDTOs;
using Application.ImageUploader;
using Application.Interfaces;
using Application.IServices.IProductServices;
using Application.ResultFolder;
using Domain.Entities.CategoryEntities;
using Domain.Entities.ProductEntities;

namespace Infrastructure.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IImageUploader _imageUploader;

    public ProductService(IUnitOfWork unitOfWork, IImageUploader imageUploader)
    {
        _unitOfWork = unitOfWork;
        _imageUploader = imageUploader;
    }

    public async Task<Result> CreateAsync(CreateProductDTO dto)
    {
        try
        {
            var imageURL = await _imageUploader.UploadImageAsync(dto.Image);
            var Product = new Product
            {
                ProductName = dto.ProductName,
                CategoryId  = dto.ProductCategoryId,
                ImageURL = imageURL,
                ProductDescription = dto.ProductDescription,
                ProductPrice = dto.ProductPrice,
            };

            await _unitOfWork.Repository<Product>().AddAsync(Product);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Product added successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to create Product: " + ex.Message);
        }
    }

    public async Task<Result> DeleteAsync(int id)
    {
        try
        {
            var repo = _unitOfWork.Repository<Product>();
            var existing = await repo.GetByIdAsync(id);

            if (existing == null)
                return Result.Failure("Product not found.", status: StatusResult.NotExists);

            repo.Delete(existing);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Product deleted successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to Delete Product: " + ex.Message);
        }
    }

    public async Task<Result> GetAllAsync()
    {
        try
        {

            var products = await _unitOfWork.Repository<Product>()
                .GetAllAsync(includes:b=>b.Category);

            var productsDTO = products.Select(b => new ProductDTO
            {
                Id = b.Id,
                ProductPrice = b.ProductPrice,
                ProductDescription = b.ProductDescription,
                ImageURL = b.ImageURL,
                ProductCategory = b.Category.Name,
                ProductName = b.ProductName
            }).ToList();

            return Result.Success("Products retrieved successfully.", productsDTO);
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to retrieve Products: " + ex.Message);
        }
    }


    public async Task<Result> GetByIdAsync(int id)
    {
        try
        {
            var repo = _unitOfWork.Repository<Product>();
            var existing = await repo.GetByIdAsync(id);

            if (existing == null)
                return Result.Failure("Product not found.", status: StatusResult.NotExists);

            var product = new ProductDTO
            {
                Id = existing.Id,
                ImageURL= existing.ImageURL,
                ProductCategory= existing.Category.Name,
                ProductDescription= existing.ProductDescription,
                ProductName= existing.ProductName,
                ProductPrice = existing.ProductPrice,
            };

            return Result.Success("Product retrieved successfully.", product);
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to Get product: " + ex.Message);
        }
    }

    public async Task<Result> SearchAsync(string productName)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(productName))
                return Result.Failure("product name is required.");

            var name = productName.Trim().ToLowerInvariant();

            var Products = await _unitOfWork.Repository<Product>()
                .WhereAsync(b=> b.ProductName.ToLower().Contains(productName));

            if (!Products.Any())
                return Result.Failure($"No Products found.", status: StatusResult.NotExists);

            var ProductsDto = Products.Select(b => new ProductDTO
            {
                Id = b.Id,
                ProductName = b.ProductName,
                ProductDescription = b.ProductDescription,
                ImageURL = b.ImageURL,
                ProductPrice= b.ProductPrice,
                ProductCategory = b.Category.Name,
            });

           
            return Result.Success($"Products retrieved successfully.", ProductsDto);
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to retrieve Products: " + ex.Message);
        }
    }

    public async Task<Result> UpdateAsync(UpdateProductDTO dto)
    {
        try
        {
            var repo = _unitOfWork.Repository<Product>();
            var existing = await repo.GetByIdAsync(dto.Id);

            if (existing == null)
                return Result.Failure("Product not found.", status: StatusResult.NotExists);

            existing.ProductName = dto.ProductName;
            existing.ProductDescription = dto.ProductDescription;
            existing.ProductPrice = dto.ProductPrice;
            existing.CategoryId = dto.ProductCategoryId;
            existing.ImageURL = await _imageUploader.UploadImageAsync(dto.Image);

            repo.Update(existing);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Product deleted successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to Delete Product: " + ex.Message);
        }
    }
}
