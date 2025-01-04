using Application.Features.Auths.CreateTokenByClient;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Auth.Endpoints
{
    public static class CreateClientTokenEndpoint
    {
        public static RouteGroupBuilder CreateClientTokenEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/create-token-by-client", async (CreateTokenByClientCommand createTokenByClientCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createTokenByClientCommand);
                return result.ToEndpointResult();
            }).WithName("CreateTokenByClient");

            return routeGroupBuilder;
        }
    }
}
