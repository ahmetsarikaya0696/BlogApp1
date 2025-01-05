using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.RefreshTokens
{
    public class RefreshTokenRepository(AppDbContext appDbContext) : GenericRepository<RefreshToken>(appDbContext), IRefreshTokenRepository
    {
    }
}
