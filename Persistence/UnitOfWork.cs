using Application.Contracts.Persistence;

namespace Persistence
{
    public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync() => await appDbContext.SaveChangesAsync();
    }
}
