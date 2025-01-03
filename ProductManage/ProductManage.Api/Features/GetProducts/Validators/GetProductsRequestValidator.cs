using FluentValidation;
using ProductManage.Api.Dtos;

namespace ProductManage.Api.Validators;

public class GetProductsRequestValidator : AbstractValidator<GetProductsRequestDto>
{
    public GetProductsRequestValidator()
    {
        RuleFor(r => r.Page)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(r => r.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");

        RuleFor(r => r.PriceMin)
            .GreaterThanOrEqualTo(0).WithMessage("Minimum price must be greater than or equal to 0.")
            .When(r => r.PriceMin.HasValue);

        RuleFor(r => r.PriceMax)
            .GreaterThan(r => r.PriceMin.Value).WithMessage("Maximum price must be greater than the minimum price.")
            .When(r => r.PriceMin.HasValue && r.PriceMax.HasValue);

        RuleFor(r => r.SortOrder)
            .Must(order => order == "asc" || order == "desc")
            .WithMessage("Sort order must be either 'asc' or 'desc'.");

        RuleFor(r => r.SortBy)
            .NotEmpty().WithMessage("Sort by field cannot be empty.")
            .When(r => !string.IsNullOrEmpty(r.SortBy));
    }
}
