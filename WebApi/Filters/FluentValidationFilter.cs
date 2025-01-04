using FluentValidation;

namespace WebApi.Filters
{
    public class FluentValidationFilter : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            // Sonu "Command" veya "Request" ile biten ilk argümanı bul
            var targetArgument = context.Arguments
                .FirstOrDefault(arg => arg?.GetType().Name.EndsWith("Command") == true ||
                                       arg?.GetType().Name.EndsWith("Dto") == true);

            if (targetArgument is null) return await next(context);

            // Validatörün tipini dinamik olarak belirle
            var validatorType = typeof(IValidator<>).MakeGenericType(targetArgument.GetType());
            var validator = context.HttpContext.RequestServices.GetService(validatorType) as dynamic;

            // Eğer validatör yoksa bir sonraki middleware'e geç
            if (validator is null) return await next(context);

            // Validasyonu çalıştır
            var validationResult = await validator.ValidateAsync((dynamic)targetArgument);

            // Validasyon başarısızsa ValidationProblem sonucu dön
            if (!validationResult.IsValid) return Results.ValidationProblem(validationResult.ToDictionary());

            // Eğer her şey yolundaysa bir sonraki middleware'e geç
            return await next(context);
        }
    }

}
