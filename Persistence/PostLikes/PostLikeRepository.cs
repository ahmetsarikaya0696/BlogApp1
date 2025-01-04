using Application.Contracts.Persistence;
using Domain.Entities;

namespace Persistence.PostLikes
{
    public class PostLikeRepository(AppDbContext appDbContext) : GenericRepository<PostLike>(appDbContext), IPostLikeRepository
    {
        public async Task<PostLike?> GetByPostIdAndUserIdAsync(Guid postId, string userId)
        {
            var postLike = await _appDbContext.PostLikes.FindAsync(postId, userId);
            return postLike;
        }
    }
}
