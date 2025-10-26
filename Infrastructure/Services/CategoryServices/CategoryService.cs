using Application.DTOs.ProductDTOs;
using Application.Interfaces;
using Application.IServices.ICategoryServices;
using Application.ResultFolder;
using Domain.Entities.CategoryEntities;

namespace Infrastructure.Services.CategoryServices;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> CreateAsync(string CategoryName)
    {
        try
        {
            var category = new Category
            {
               Name = CategoryName,
            };

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return Result.Success("Category added successfully.");
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to create Category: " + ex.Message);
        }
    }

    public async Task<Result> GetCategoryListAsync()
    {
        try
        {

            var category = await _unitOfWork.Repository<Category>()
                .GetAllAsync();


            return Result.Success("Category retrieved successfully.", category);
        }
        catch (Exception ex)
        {
            return Result.Failure("Failed to retrieve Categories: " + ex.Message);
        }
    }
}
