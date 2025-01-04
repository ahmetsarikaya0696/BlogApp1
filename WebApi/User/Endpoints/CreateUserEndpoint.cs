using Application.Features.Users.Create;
using MediatR;
using WebApi.Extensions;

namespace WebApi.User.Endpoints
{
    public static class CreateUserEndpoint
    {
        public static RouteGroupBuilder CreateUserEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/", async (CreateUserCommand createUserCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createUserCommand);
                return result.ToEndpointResult();
            }).WithName("CreateUser");

            return routeGroupBuilder;
        }
    }
}
