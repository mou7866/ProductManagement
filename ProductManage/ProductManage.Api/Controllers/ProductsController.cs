using Microsoft.AspNetCore.Mvc;
using ProductManage.Api.Dtos;
using ProductManage.Api.Services;

namespace ProductManage.Api.Controllers;

public static class ProductsController
{
    public static void MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
                       .AddEndpointFilter<GlobalValidationFilter>()
                       .WithTags("Products");

        group.MapGet("/", GetAllProducts).WithName("GetAllProducts");

        group.MapGet("/{id:guid}", GetProductById).WithName("GetProductById");

        group.MapPost("/", CreateProduct).WithName("CreateProduct");

        group.MapPut("/{id:guid}", UpdateProduct).WithName("UpdateProduct");

        group.MapDelete("/{id:guid}", DeleteProduct).WithName("DeleteProduct");
    }

    private static async Task<IResult> GetAllProducts([FromServices] IGetProductsService getProductsService, [AsParameters] GetProductsRequestDto request)
    {
        var products = await getProductsService.GetProductsAsync(request);
        return Results.Ok(products);
    }

    private static async Task<IResult> GetProductById([FromServices] IGetProductsService getProductsService, Guid id)
    {
        var product = await getProductsService.GetProductByIdAsync(id);

        return product is not null ? Results.Ok(product) : Results.NotFound();
    }

    private static async Task<IResult> CreateProduct([FromServices] IProductsService manageProductsService, [FromBody] CreateProductDto productDto)
    {
        var createdProduct = await manageProductsService.CreateProductAsync(productDto);
        return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
    }

    private static async Task<IResult> UpdateProduct([FromServices] IProductsService manageProductsService, Guid id, [FromBody] UpdateProductDto productDto)
    {
        var updated = await manageProductsService.UpdateProductAsync(id, productDto);
        return updated ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> DeleteProduct([FromServices] IProductsService manageProductsService, Guid id)
    {
        var deleted = await manageProductsService.DeleteProductAsync(id);
        return deleted ? Results.NoContent() : Results.Conflict();
    }
}
