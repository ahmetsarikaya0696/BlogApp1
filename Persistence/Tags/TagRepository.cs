using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.Tags
{
    public class TagRepository(AppDbContext appDbContext) : GenericRepository<Tag>(appDbContext), ITagRepository
    {
    }
}
