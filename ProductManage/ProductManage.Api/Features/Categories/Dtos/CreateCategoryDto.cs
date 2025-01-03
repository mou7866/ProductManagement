namespace ProductManage.Api.Dtos;

public class CreateCategoryDto
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string Status { get; set; } = "Active";
}
