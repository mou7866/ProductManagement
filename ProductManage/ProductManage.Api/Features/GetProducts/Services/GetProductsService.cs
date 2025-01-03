using ProductManage.Api.Dtos;
using ProductManage.Api.Repositories;

namespace ProductManage.Api.Services;

public class GetProductsService(IProductRepository productRepository) : IGetProductsService
{
    public async Task<GetProductsResponseDto> GetProductsAsync(GetProductsRequestDto request)
    {
        var products = await productRepository.GetFilteredAsync(
            categoryId: request.CategoryId,
            priceMin: request.PriceMin,
            priceMax: request.PriceMax,
            status: request.Status,
            search: request.Search,
            sortBy: request.SortBy ?? "Name",
            sortOrder: request.SortOrder?.ToLowerInvariant(),
            page: request.Page,
            pageSize: request.PageSize
        );

        var totalItems = await productRepository.GetFilteredCountAsync(
            categoryId: request.CategoryId,
            priceMin: request.PriceMin,
            priceMax: request.PriceMax,
            status: request.Status,
            search: request.Search
        );

        var productDtos = products.Select(p => new ProductDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            Status = p.Status.ToString(),
            StockQuantity = p.StockQuantity,
            ImageUrl = p.ImageUrl,
            CategoryName = p.Category?.Name ?? "Unknown"
        }).ToList();

        return new GetProductsResponseDto
        {
            Products = productDtos,
            TotalItems = totalItems,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }

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
}