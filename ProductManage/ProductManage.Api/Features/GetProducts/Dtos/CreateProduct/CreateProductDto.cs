namespace ProductManage.Api.Dtos;

public class CreateProductDto
{
    public string Name { get; set; } = default!;

    public string Description { get; set; } = default!;

    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public string Status { get; set; } = "Active";

    public int StockQuantity { get; set; }

    public string? ImageUrl { get; set; }
}