using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Tags
{
    public class TagRepository(AppDbContext appDbContext) : GenericRepository<Tag>(appDbContext), ITagRepository
    {
    }
}
