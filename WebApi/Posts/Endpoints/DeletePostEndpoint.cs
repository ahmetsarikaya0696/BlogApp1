using Application.Features.Posts.Delete;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Posts.Endpoints
{
    public static class DeletePostEndpoint
    {
        public static RouteGroupBuilder DeletePostEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeletePostCommand(id));
                return result.ToEndpointResult();
            }).WithName("DeletePost");
            //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
