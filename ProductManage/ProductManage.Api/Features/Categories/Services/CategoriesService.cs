using ProductManage.Api.Dtos;
using ProductManage.Api.Models;
using ProductManage.Api.Repositories;

namespace ProductManage.Api.Services;

public class CategoriesService(ICategoryRepository categoryRepository, IProductRepository productRepository) 
    : ICategoriesService
{
    public async Task<CategoryDto> CreateCategoryAsync(CreateCategoryDto categoryDto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = categoryDto.Name,
            Description = categoryDto.Description,
            Status = categoryDto.Status,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await categoryRepository.AddAsync(category);

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

    public async Task<bool> UpdateCategoryAsync(Guid id, UpdateCategoryDto categoryDto)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category == null) return false;

        category.Name = categoryDto.Name;
        category.Description = categoryDto.Description;
        category.Status = categoryDto.Status;

        await categoryRepository.UpdateAsync(category);
        return true;
    }

    public async Task<bool> DeleteCategoryAsync(Guid id)
    {
        var category = await categoryRepository.GetByIdAsync(id);

        if (category == null) return false;

        var products = await productRepository.GetByCategoryIdAsync(id);

        if (products.Any()) return false;

        await categoryRepository.DeleteAsync(category);

        return true;
    }
}
