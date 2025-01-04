using ProductManage.Api.Dtos;

namespace ProductManage.Api.Services;

public interface IProductsService
{
    Task<ProductDto?> GetProductByIdAsync(Guid id);

    Task<ProductDto> CreateProductAsync(CreateProductDto productDto);

    Task<bool> UpdateProductAsync(Guid id, UpdateProductDto productDto);

    Task<bool> DeleteProductAsync(Guid id);
}