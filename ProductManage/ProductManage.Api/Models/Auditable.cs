using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManage.Api.Models;

public abstract class Auditable
{
    [Key]
    [Column("id")]
    public Guid Id { get; set; }

    [Column("createddate")]
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    
    [Column("updateddate")]
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
}
