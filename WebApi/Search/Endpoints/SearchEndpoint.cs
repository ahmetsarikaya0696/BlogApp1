using Application.Features.Auths.CreateTokenByClient;
using Application.Features.Search;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Search.Endpoints
{
    public static class SearchEndpoint
    {
        public static RouteGroupBuilder SearchEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapPost("/", async (SearchCommand searchCommand, IMediator mediator) =>
            {
                var result = await mediator.Send(searchCommand);
                return result.ToEndpointResult();
            }).WithName("Search");

            return routeGroupBuilder;
        }
    }
}
