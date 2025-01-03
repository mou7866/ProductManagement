using Microsoft.AspNetCore.Mvc;
using ProductManage.Api.Dtos;
using ProductManage.Api.Services;

namespace ProductManage.Api.Controllers;

public static class CategoriesController
{
    public static void MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/categories")
                       .WithTags("Categories");

        group.MapGet("/", GetAllCategories).WithName("GetAllCategories");

        group.MapGet("/{id:guid}", GetCategoryById).WithName("GetCategoryById");

        group.MapPost("/", CreateCategory).WithName("CreateCategory");

        group.MapPut("/{id:guid}", UpdateCategory).WithName("UpdateCategory");

        group.MapDelete("/{id:guid}", DeleteCategory).WithName("DeleteCategory");
    }

    private static async Task<IResult> GetAllCategories([FromServices] IGetCategoriesService getCategoriesService)
    {
        var categories = await getCategoriesService.GetAllCategoriesAsync();
        return Results.Ok(categories);
    }

    private static async Task<IResult> GetCategoryById([FromServices] IGetCategoriesService getCategoriesService, Guid id)
    {
        var category = await getCategoriesService.GetCategoryByIdAsync(id);
        return category is not null ? Results.Ok(category) : Results.NotFound();
    }

    private static async Task<IResult> CreateCategory([FromServices] ICategoriesService manageCategoriesService, [FromBody] CreateCategoryDto categoryDto)
    {
        var createdCategory = await manageCategoriesService.CreateCategoryAsync(categoryDto);
        return Results.Created($"/api/categories/{createdCategory.Id}", createdCategory);
    }

    private static async Task<IResult> UpdateCategory([FromServices] ICategoriesService manageCategoriesService, Guid id, [FromBody] UpdateCategoryDto categoryDto)
    {
        var updated = await manageCategoriesService.UpdateCategoryAsync(id, categoryDto);
        return updated ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> DeleteCategory([FromServices] ICategoriesService manageCategoriesService, Guid id)
    {
        var deleted = await manageCategoriesService.DeleteCategoryAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}
