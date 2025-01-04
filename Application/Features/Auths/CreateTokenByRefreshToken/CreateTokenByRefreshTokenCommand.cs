using Application.Dtos;
using MediatR;

namespace Application.Features.Auths.CreateTokenByRefreshToken
{
    public record CreateTokenByRefreshTokenCommand(string RefreshToken) : IRequest<ServiceResult<TokenDto>>;
}
