namespace ProductManage.Api.Dtos;

public class UpdateCategoryDto
{
    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public string Status { get; set; } = "Active";
}
