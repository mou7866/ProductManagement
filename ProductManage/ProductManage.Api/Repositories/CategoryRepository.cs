using Microsoft.EntityFrameworkCore;
using ProductManage.Api.Data;
using ProductManage.Api.Models;

namespace ProductManage.Api.Repositories;

public class CategoryRepository(ProductManagementDBContext productManagementDBContext) 
    : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await productManagementDBContext.Categories.AsNoTracking().ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await productManagementDBContext.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Category category)
    {
        await productManagementDBContext.Categories.AddAsync(category);
        await productManagementDBContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        productManagementDBContext.Categories.Update(category);
        await productManagementDBContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Category category)
    {
        productManagementDBContext.Categories.Remove(category);
        await productManagementDBContext.SaveChangesAsync();
    }
}