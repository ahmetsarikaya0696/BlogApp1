using Application.Features.Auths.RevokeRefreshToken;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Auth.Endpoints
{
    public static class RevokeRefreshTokenEndpoint
    {
        public static RouteGroupBuilder RevokeRefreshTokenEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/revoke-refresh-token", async (RevokeRefreshTokenCommand revokeRefreshTokenCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(revokeRefreshTokenCommand);
                return result.ToEndpointResult();
            }).WithName("RevokeRefreshToken");

            return routeGroupBuilder;
        }
    }
}
