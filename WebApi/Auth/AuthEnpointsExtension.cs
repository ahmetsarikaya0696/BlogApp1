using WebApi.Auth.Endpoints;

namespace WebApi.Auth
{
    public static class AuthEnpointsExtension
    {
        public static void AddAuthEnpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/auth")
               .WithTags("Auth")
               .CreateTokenEndpointExtension()
               .CreateClientTokenEndpointExtension()
               .RevokeRefreshTokenEndpointExtension()
               .CreateTokenByRefreshTokenEndpointExtension();
        }
    }
}
