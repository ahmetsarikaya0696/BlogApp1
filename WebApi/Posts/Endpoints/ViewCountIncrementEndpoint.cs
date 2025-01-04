using Application.Features.Posts.IncrementViewCount;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class ViewCountIncrementEndpoint
    {
        public static RouteGroupBuilder ViewCountIncrementEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPatch("/{id:guid}/viewcount", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new IncrementPostViewCountCommand(id));
                return result.ToEndpointResult();
            }).WithName("ViewCountIncrementEndpoint");

            return routeGroupBuilder;
        }
    }
}
