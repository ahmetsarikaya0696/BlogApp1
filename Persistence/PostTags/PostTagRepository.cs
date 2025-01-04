using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.PostTags
{
    public class PostTagRepository(AppDbContext appDbContext) : GenericRepository<PostTag>(appDbContext), IPostTagRepository
    {

    }
}
