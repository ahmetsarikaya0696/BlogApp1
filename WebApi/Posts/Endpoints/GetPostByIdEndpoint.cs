using Application.Features.Posts.GetById;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class GetPostByIdEndpoint
    {
        public static RouteGroupBuilder GetPostByIdEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new GetPostByIdQuery(id));
                return result.ToEndpointResult();
            }).WithName("GetPostById");

            return routeGroupBuilder;
        }
    }
}
