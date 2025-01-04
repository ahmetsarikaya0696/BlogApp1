using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.Posts
{
    public class PostRepository(AppDbContext appDbContext) : GenericRepository<Post>(appDbContext), IPostRepository
    {
    }
}
