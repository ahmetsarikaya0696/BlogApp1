using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Persistence
{
    public class GenericRepository<T>(AppDbContext appDbContext) : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _appDbContext = appDbContext;
        private readonly DbSet<T> _dbSet = appDbContext.Set<T>();

        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<T> GetAll() => _dbSet.AsNoTracking();

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);

        public void Update(T entity) => _dbSet.Update(entity);

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => await _dbSet.AnyAsync(predicate);

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) => await _dbSet.CountAsync(predicate);

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);
    }
}
