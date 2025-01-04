using MediatR;

namespace Application.Features.Auths.RevokeRefreshToken
{
    public record RevokeRefreshTokenCommand(string RefreshToken) : IRequest<ServiceResult>;
}
