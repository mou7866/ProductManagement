using ProductManage.Api.Dtos;
using ProductManage.Api.Repositories;

namespace ProductManage.Api.Services;

public class GetCategoriesService(ICategoryRepository categoryRepository) : IGetCategoriesService
{
    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Status = c.Status.ToString(),
            CreatedDate = c.CreatedDate,
            UpdatedDate = c.UpdatedDate
        });
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);
        if (category == null) return null;

        return new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            Status = category.Status.ToString(),
            CreatedDate = category.CreatedDate,
            UpdatedDate = category.UpdatedDate
        };
    }
}
