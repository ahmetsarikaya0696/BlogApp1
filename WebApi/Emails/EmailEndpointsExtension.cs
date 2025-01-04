using WebApi.Emails.Endpoints;
using WebApi.Filters;

namespace WebApi.Emails
{
    public static class EmailEndpointsExtension
    {
        public static void AddEmailEndpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/email")
               .WithTags("email")
               .ConfirmEmailEndpointExtension()
               .AddEndpointFilter<FluentValidationFilter>();
        }
    }
}
