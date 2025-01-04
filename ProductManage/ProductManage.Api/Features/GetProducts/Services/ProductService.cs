using ProductManage.Api.Dtos;
using ProductManage.Api.Models;
using ProductManage.Api.Repositories;

namespace ProductManage.Api.Services;

public class ProductsService(IProductRepository productRepository, ICategoryRepository categoryRepository) : IProductsService
{
    public async Task<ProductDto?> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null) return null;

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Status = product.Status.ToString(),
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl,
            CategoryName = product.Category?.Name ?? "Unknown"
        };
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductDto productDto)
    {
        var category = await categoryRepository.GetByIdAsync(productDto.CategoryId) ?? throw new ArgumentException("Invalid category ID");

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            CategoryId = productDto.CategoryId,
            Status = productDto.Status,
            StockQuantity = productDto.StockQuantity,
            ImageUrl = productDto.ImageUrl,
            CreatedDate = DateTime.UtcNow,
            UpdatedDate = DateTime.UtcNow
        };

        await productRepository.AddAsync(product);

        return new ProductDto
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Status = product.Status.ToString(),
            StockQuantity = product.StockQuantity,
            ImageUrl = product.ImageUrl,
            CategoryName = category.Name
        };
    }

    public async Task<bool> UpdateProductAsync(Guid id, UpdateProductDto productDto)
    {
        var product = await productRepository.GetByIdAsync(id);

        if (product == null) return false;

        _ = await categoryRepository.GetByIdAsync(productDto.CategoryId) ?? throw new ArgumentException("Invalid category ID");

        product.Name = productDto.Name;
        product.Description = productDto.Description;
        product.Price = productDto.Price;
        product.CategoryId = productDto.CategoryId;
        product.Status = productDto.Status;
        product.StockQuantity = productDto.StockQuantity;
        product.ImageUrl = productDto.ImageUrl;
        product.UpdatedDate = DateTime.UtcNow;

        await productRepository.UpdateAsync(product);
        return true;
    }

    public async Task<bool> DeleteProductAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null) return false;

        await productRepository.DeleteAsync(product);
        return true;
    }
}
