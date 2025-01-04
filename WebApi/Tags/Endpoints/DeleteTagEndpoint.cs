using Application.Features.Tags.Delete;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Tags.Endpoints
{
    public static class DeleteTagEndpoint
    {
        public static RouteGroupBuilder DeleteTagEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapDelete("/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var result = await mediator.Send(new DeleteTagCommand(id));
                return result.ToEndpointResult();
            }).WithName("DeleteTag");
              //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
