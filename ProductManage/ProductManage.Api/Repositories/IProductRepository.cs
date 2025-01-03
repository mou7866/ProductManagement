using ProductManage.Api.Models;

namespace ProductManage.Api.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId);

    Task<IEnumerable<Product>> GetAllAsync();

    Task<Product?> GetByIdAsync(Guid id);

    Task<IEnumerable<Product>> GetFilteredAsync(
        Guid? categoryId, decimal? priceMin, decimal? priceMax, string? status, string? search, string sortBy, string sortOrder, int page, int pageSize);

    Task<int> GetFilteredCountAsync(
        Guid? categoryId, decimal? priceMin, decimal? priceMax, string? status, string? search);

    Task AddAsync(Product product);

    Task UpdateAsync(Product product);

    Task DeleteAsync(Product product);

    Task DeleteRangeAsync(IEnumerable<Product> products);
}
