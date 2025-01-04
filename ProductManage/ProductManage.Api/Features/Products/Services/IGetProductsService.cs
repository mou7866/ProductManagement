using ProductManage.Api.Dtos;

namespace ProductManage.Api.Services;

public interface IGetProductsService
{
    Task<GetProductsResponseDto> GetProductsAsync(GetProductsRequestDto request);

    Task<ProductDto?> GetProductByIdAsync(Guid id);
}
