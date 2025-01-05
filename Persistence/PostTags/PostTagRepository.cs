using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.PostTags
{
    public class PostTagRepository(AppDbContext appDbContext) : GenericRepository<PostTag>(appDbContext), IPostTagRepository
    {

    }
}
