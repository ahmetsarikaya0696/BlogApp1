using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.RefreshTokens
{
    public class RefreshTokenRepository(AppDbContext appDbContext) : GenericRepository<RefreshToken>(appDbContext), IRefreshTokenRepository
    {
    }
}
