using Application.Contracts.Persistence;
using Persistence.Data;

namespace Persistence
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync() => await appDbContext.SaveChangesAsync();
    }
}
