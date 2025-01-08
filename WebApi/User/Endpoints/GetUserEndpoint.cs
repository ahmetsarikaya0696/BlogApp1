using Application.Features.Users.GetByUserName;
using MediatR;
using WebApi.Extensions;

namespace WebApi.User.Endpoints
{
    public static class GetUserEndpoint
    {
        public static RouteGroupBuilder GetUserEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/", async (IMediator mediator, HttpContext httpContext) =>
            {
                var userName = httpContext.User.Identity!.Name!;
                var result = await mediator.Send(new GetUserByUserNameQuery(userName));
                return result.ToEndpointResult();
            }).WithName("GetUserByUserName");
            //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
