using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManage.Api.Models;

[Table("products")]
public class Product : Auditable
{
    [Required]
    [MaxLength(100)]
    [Column("name")]
    public string Name { get; set; } = default!;

    [Required]
    [MaxLength(500)]
    [Column("description")]
    public string Description { get; set; } = default!;

    [Required]
    [Column("price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Required]
    [Column("categoryid")]
    [ForeignKey("category")]
    public Guid CategoryId { get; set; }

    [Required]
    [Column("status")]
    public string Status { get; set; } = ProductStatus.Active.ToString();

    [Required]
    [Range(0, int.MaxValue)]
    [Column("stockquantity")]
    public int StockQuantity { get; set; } = 0;

    [Column("imageurl")]
    public string? ImageUrl { get; set; }

    public Category? Category { get; set; }
}