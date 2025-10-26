using Application.DTOs.ProductDTOs;
using Application.ResultFolder;

namespace Application.IServices.IProductServices;

public interface IProductService
{
    Task<Result> CreateAsync(CreateProductDTO dto);
    Task<Result> GetAllAsync();
    Task<Result> GetByIdAsync(int id);
    Task<Result> UpdateAsync(UpdateProductDTO dto);
    Task<Result> DeleteAsync(int id);
    Task<Result> SearchAsync(string productName);
}
