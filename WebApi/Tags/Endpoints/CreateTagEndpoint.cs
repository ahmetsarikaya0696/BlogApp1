using Application.Features.Tags.Create;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Tags.Endpoints
{
    public static class CreateTagEndpoint
    {
        public static RouteGroupBuilder CreateTagEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/", async (CreateTagCommand createTagCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(createTagCommand);
                return result.ToEndpointResult();
            }).WithName("CreateTag");
              //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
