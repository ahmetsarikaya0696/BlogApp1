using Application.Features.Auths.CreateTokenByRefreshToken;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Auth.Endpoints
{
    public static class CreateTokenByRefreshTokenEndpoint
    {
        public static RouteGroupBuilder CreateTokenByRefreshTokenEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/create-token-by-refresh-token", async (CreateTokenByRefreshTokenCommand createTokenByRefreshTokenCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createTokenByRefreshTokenCommand);
                return result.ToEndpointResult();
            }).WithName("CreateTokenByRefreshToken");

            return routeGroupBuilder;
        }
    }
}
