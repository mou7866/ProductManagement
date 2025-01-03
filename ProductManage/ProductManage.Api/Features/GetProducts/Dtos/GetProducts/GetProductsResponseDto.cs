namespace ProductManage.Api.Dtos;

public class GetProductsResponseDto
{
    public IEnumerable<ProductDto> Products { get; set; } = [];

    public int TotalItems { get; set; }

    public int Page { get; set; }

    public int PageSize { get; set; }
}

public class ProductDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public string Status { get; set; } = default!;

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public string CategoryName { get; set; } = default!;
}

