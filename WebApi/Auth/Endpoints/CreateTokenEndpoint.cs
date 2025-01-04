using Application.Features.Auths.CreateToken;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Auth.Endpoints
{
    public static class CreateTokenEndpoint
    {
        public static RouteGroupBuilder CreateTokenEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/create-token", async (CreateTokenCommand createTokenCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createTokenCommand);
                return result.ToEndpointResult();
            }).WithName("CreateToken");

            return routeGroupBuilder;
        }
    }
}
