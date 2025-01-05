using Application.Contracts.Persistence;
using Domain.Entities;
using Persistence.Data;

namespace Persistence.Posts
{
    public class PostRepository(AppDbContext appDbContext) : GenericRepository<Post>(appDbContext), IPostRepository
    {
    }
}
