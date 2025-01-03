using Microsoft.EntityFrameworkCore;
using ProductManage.Api.Data;

namespace ProductManage.Api;

public static class DatabaseMigrationService
{
    public static void MigrateDatabase(this IHost app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var dbContext = services.GetRequiredService<ProductManagementDBContext>();

        try
        {
            dbContext.Database.Migrate();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Database migration failed: {ex.Message}");
            throw;
        }
    }
}