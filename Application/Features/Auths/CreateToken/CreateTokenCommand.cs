using Application.Dtos;
using MediatR;

namespace Application.Features.Auths.CreateToken
{
    public record CreateTokenCommand(string Email, string Password) : IRequest<ServiceResult<TokenDto>>;
}
