using Application.Features.Posts.Update;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class UpdatePostEndpoint
    {
        public static RouteGroupBuilder UpdatePostEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPut("/", async (UpdatePostCommand updatePostCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(updatePostCommand);
                return result.ToEndpointResult();
            }).WithName("UpdatePost");
              //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
