namespace ProductManage.Api.Dtos;

public class GetProductsRequestDto
{
    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public Guid? CategoryId { get; set; }

    public decimal? PriceMin { get; set; }

    public decimal? PriceMax { get; set; }

    public string? Status { get; set; }

    public string? Search { get; set; }

    public string? SortBy { get; set; } = "Name";

    public string SortOrder { get; set; } = "asc";
}
