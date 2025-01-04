using Microsoft.EntityFrameworkCore;
using ProductManage.Api.Data;
using ProductManage.Api.Models;

namespace ProductManage.Api.Repositories;

public class ProductRepository(ProductManagementDBContext productManagementDBContext) 
    : IProductRepository
{
    public async Task<IEnumerable<Product>> GetByCategoryIdAsync(Guid categoryId)
    {
        return await productManagementDBContext.Products
            .Where(p => p.CategoryId == categoryId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await productManagementDBContext
            .Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await productManagementDBContext
            .Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Product>> GetFilteredAsync(
        Guid? categoryId, 
        decimal? priceMin, 
        decimal? priceMax, 
        string? status, 
        string? search, 
        string sortBy, 
        string sortOrder, 
        int page, 
        int pageSize)
    {
        var query = productManagementDBContext
            .Products
            .Include(p => p.Category)
            .AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        if (priceMin.HasValue)
            query = query.Where(p => p.Price >= priceMin);

        if (priceMax.HasValue)
            query = query.Where(p => p.Price <= priceMax);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(p => p.Status.ToString() == status);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

        query = sortOrder.Equals("asc", StringComparison.InvariantCultureIgnoreCase)
            ? productManagementDBContext.Products.OrderBy(p => p.Name)
            : productManagementDBContext.Products.OrderByDescending(p => p.Name);

        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and pageSize must be greater than zero.");
        }

        return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetFilteredCountAsync(
        Guid? categoryId, decimal? priceMin, decimal? priceMax, string? status, string? search)
    {
        var query = productManagementDBContext.Products.AsQueryable();

        if (categoryId.HasValue)
            query = query.Where(p => p.CategoryId == categoryId);

        if (priceMin.HasValue)
            query = query.Where(p => p.Price >= priceMin);

        if (priceMax.HasValue)
            query = query.Where(p => p.Price <= priceMax);

        if (!string.IsNullOrEmpty(status))
            query = query.Where(p => p.Status.ToString() == status);

        if (!string.IsNullOrEmpty(search))
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));

        return await query.CountAsync();
    }

    public async Task AddAsync(Product product)
    {
        productManagementDBContext.Products.Add(product);
        await productManagementDBContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        productManagementDBContext.Products.Update(product);
        await productManagementDBContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        productManagementDBContext.Products.Remove(product);
        await productManagementDBContext.SaveChangesAsync();
    }

    public async Task DeleteRangeAsync(IEnumerable<Product> products)
    {
        productManagementDBContext.Products.RemoveRange(products);
        await productManagementDBContext.SaveChangesAsync();
    }
}

