using Application.ResultFolder;

namespace Application.IServices.ICategoryServices;

public interface ICategoryService
{
    Task<Result> CreateAsync(string CategoryName);
    Task<Result> GetCategoryListAsync();
}
