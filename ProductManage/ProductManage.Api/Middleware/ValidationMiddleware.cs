using FluentValidation;

namespace ProductManage.Api;

public class GlobalValidationFilter(IServiceProvider serviceProvider) : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        foreach (var argument in context.Arguments)
        {
            if (argument is null) continue;

            var argumentType = argument.GetType();

            var validatorType = typeof(IValidator<>).MakeGenericType(argumentType);

            var validator = serviceProvider.GetService(validatorType) as IValidator;

            if (validator is not null)
            {
                var validationResult = await validator.ValidateAsync(new ValidationContext<object>(argument));

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage });

                    return Results.BadRequest(new { Errors = errors });
                }
            }
        }

        return await next(context);
    }
}