using Microsoft.EntityFrameworkCore;
using ProductManage.Api.Models;

namespace ProductManage.Api.Data;

public class ProductManagementDBContext(DbContextOptions<ProductManagementDBContext> options) 
    : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;

    public DbSet<Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(100);

            entity.Property(p => p.Description).IsRequired().HasMaxLength(500);

            entity.Property(p => p.Price).IsRequired().HasPrecision(18,2);

            entity.Property(p => p.StockQuantity).IsRequired();

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
            
            entity.Property(e => e.CreatedDate)
                  .HasColumnType("timestamp with time zone")
                  .IsRequired();

            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(50);

            entity.HasIndex(c => c.Name).IsUnique(); 

            entity.Property(c => c.Description).HasMaxLength(200);

            entity.Property(e => e.CreatedDate)
                  .HasColumnType("timestamp with time zone")
                  .IsRequired();

            entity.Property(e => e.UpdatedDate)
                .HasColumnType("timestamp with time zone")
                .IsRequired();
        });
    }
}
