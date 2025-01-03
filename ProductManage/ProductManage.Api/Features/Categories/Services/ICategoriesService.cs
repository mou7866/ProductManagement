using ProductManage.Api.Dtos;

namespace ProductManage.Api.Services;

public interface ICategoriesService
{
    Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto);

    Task<bool> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto);

    Task<bool> DeleteCategoryAsync(Guid id);
}
