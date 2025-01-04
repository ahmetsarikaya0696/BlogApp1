using WebApi.Filters;
using WebApi.User.Endpoints;

namespace WebApi.User
{
    public static class UserEndpointsExtension
    {
        public static void AddUserEnpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/users")
               .WithTags("Users")
               .CreateUserEndpointExtension()
               .GetUserEndpointExtension()
               .AddEndpointFilter<FluentValidationFilter>(); ;
        }
    }
}
