using Application.Features.Posts.Create;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class CreatePostEndpoint
    {
        public static RouteGroupBuilder CreatePostEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/", async (CreatePostCommand createPostCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createPostCommand);
                return result.ToEndpointResult();
            }).WithName("CreatePost");
            //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
