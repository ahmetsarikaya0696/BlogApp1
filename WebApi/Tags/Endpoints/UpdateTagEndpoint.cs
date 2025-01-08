using Application.Features.Tags.Update;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Tags.Endpoints
{
    public static class UpdateTagEndpoint
    {
        public static RouteGroupBuilder UpdateTagEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPut("/", async (UpdateTagCommand updateTagCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(updateTagCommand);
                return result.ToEndpointResult();
            }).WithName("UpdateTag");
            //.RequireAuthorization();

            return routeGroupBuilder;
        }
    }
}
