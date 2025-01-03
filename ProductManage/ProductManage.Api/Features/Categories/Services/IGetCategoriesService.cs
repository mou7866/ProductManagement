using ProductManage.Api.Dtos;

namespace ProductManage.Api.Services;

public interface IGetCategoriesService
{
    Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();

    Task<CategoryDto?> GetCategoryByIdAsync(Guid id);
}
