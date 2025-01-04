using Application.Dtos;
using MediatR;

namespace Application.Features.Auths.CreateTokenByClient
{
    public record CreateTokenByClientCommand(string ClientId, string ClientSecret) : IRequest<ServiceResult<ClientTokenDto>>;
}
