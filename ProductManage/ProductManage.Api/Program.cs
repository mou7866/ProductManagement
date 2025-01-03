using FluentValidation;
using Microsoft.EntityFrameworkCore;
using ProductManage.Api.Controllers;
using ProductManage.Api.Data;
using ProductManage.Api.Dtos;
using ProductManage.Api.Repositories;
using ProductManage.Api.Services;
using ProductManage.Api.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IGetProductsService, GetProductsService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IGetCategoriesService, GetCategoriesService>();
builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddDbContext<ProductManagementDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddTransient<IValidator<CreateProductDto>, CreateProductRequestValidator>();
builder.Services.AddTransient<IValidator<UpdateProductDto>, UpdateProductRequestValidator>();
builder.Services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
builder.Services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

ProductsController.MapProductEndpoints(app);

CategoriesController.MapCategoryEndpoints(app);

app.Run();