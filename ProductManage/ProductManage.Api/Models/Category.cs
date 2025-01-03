using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManage.Api.Models;

[Table("categories")]
public class Category : Auditable
{
    [Required]
    [MaxLength(50)]
    [Column("name")]
    public string Name { get; set; } = default!;

    [MaxLength(200)]
    [Column("description")]
    public string? Description { get; set; }

    [Required]
    [Column("status")]
    public string Status { get; set; } = CategoryStatus.Active.ToString();

    public ICollection<Product>? Products { get; set; }
}