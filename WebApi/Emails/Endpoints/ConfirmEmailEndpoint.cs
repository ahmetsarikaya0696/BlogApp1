using Application.Features.Emails.Confirm;
using MediatR;
using WebApi.Extensions;

namespace WebApi.Emails.Endpoints
{
    public static class ConfirmEmailEndpoint
    {
        public static RouteGroupBuilder ConfirmEmailEndpointExtension(this RouteGroupBuilder routeGroupBuilder)
        {
            routeGroupBuilder.MapGet("/confirm", async (string userId, string token, IMediator mediator) =>
            {
                var result = await mediator.Send(new ConfirmEmailCommand(userId, token));
                return result.ToEndpointResult();
            }).WithName("ConfirmEmail");

            return routeGroupBuilder;
        }
    }
}
