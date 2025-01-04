using Application.Dtos;
using Domain.Configuration;
using Domain.Entities;

namespace Application.Contracts.Infrastructure
{
    public interface ITokenService
    {
        TokenDto CreateToken(User user);
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
