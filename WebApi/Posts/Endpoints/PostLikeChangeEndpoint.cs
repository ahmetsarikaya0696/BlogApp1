using Application.Features.Posts.Like;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class PostLikeChangeEndpoint
    {
        public static RouteGroupBuilder PostLikeChangeEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/{postId:guid}/users/{userId}", async (Guid postId, string userId, bool isLiked, IMediator mediator) =>
            {
                var result = await mediator.Send(new CreateOrDeletePostLikeCommand(postId, userId, isLiked));
                return result.ToEndpointResult();
            }).WithName("PostLikeChange");

            return routeGroupBuilder;
        }
    }
}
