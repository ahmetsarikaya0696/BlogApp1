using Application.Features.Tags.GetAll;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Tags.Endpoints
{
    public static class GetAllTagsEndpoint
    {
        public static RouteGroupBuilder GetAllTagsEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetAllTagsQuery());
                return result.ToEndpointResult();
            }).WithName("GetAllTags");

            return routeGroupBuilder;
        }
    }
}
