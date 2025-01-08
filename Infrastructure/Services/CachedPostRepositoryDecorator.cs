using Application.Contracts.Persistence;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Linq.Expressions;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CachedPostRepositoryDecorator(IPostRepository postRepository, RedisRepository redisRepository) : IPostRepository
    {
        private const string postKey = "posts";
        private readonly IDatabase cacheRepository = redisRepository.GetDatabase();

        public async Task AddAsync(Post entity)
        {
            await postRepository.AddAsync(entity);

            if (cacheRepository.KeyExists(postKey))
            {
                entity.CreatedDate = DateTime.Now;
                var jsonPost = JsonSerializer.Serialize(entity);
                await cacheRepository.HashSetAsync(postKey, entity.Id.ToString(), jsonPost);
            }
        }

        public async Task<bool> AnyAsync(Expression<Func<Post, bool>> predicate)
        {
            if (!cacheRepository.KeyExists(postKey))
            {
                var fetchedPosts = await FetchAndCacheEntitiesAsync();
                return fetchedPosts.AsQueryable().Any(predicate);
            }

            var cachedPosts = await cacheRepository.StringGetAsync(postKey);
            if (cachedPosts.IsNullOrEmpty) return false;

            var posts = JsonSerializer.Deserialize<List<Post>>(cachedPosts!);
            return posts!.AsQueryable().Any(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<Post, bool>> predicate)
        {
            if (!cacheRepository.KeyExists(postKey))
            {
                var fetchedPosts = await FetchAndCacheEntitiesAsync();
                return fetchedPosts.AsQueryable().Count(predicate);
            }

            var cachedPosts = await cacheRepository.StringGetAsync(postKey);
            if (cachedPosts.IsNullOrEmpty) return 0;

            var posts = JsonSerializer.Deserialize<List<Post>>(cachedPosts!);
            return posts!.AsQueryable().Count(predicate);
        }

        public void Delete(Post entity)
        {
            postRepository.Delete(entity);
            cacheRepository.HashDelete(postKey, entity.Id.ToString());
        }

        public IQueryable<Post> GetAll()
        {
            if (!cacheRepository.KeyExists(postKey)) return FetchAndCacheEntities().AsQueryable();

            List<Post> posts = new List<Post>();
            var cachedPosts = cacheRepository.HashGetAll(postKey).ToList();
            foreach (var cachedPost in cachedPosts)
            {
                var post = JsonSerializer.Deserialize<Post>(cachedPost.Value!);
                posts.Add(post!);
            }

            return posts.AsQueryable();
        }

        public async Task<Post?> GetByIdAsync(Guid id)
        {
            if (!cacheRepository.KeyExists(postKey))
            {
                var posts = await FetchAndCacheEntitiesAsync();
                return posts.Find(x => x.Id == id);
            }

            var cachedPost = await cacheRepository.HashGetAsync(postKey, id.ToString());
            var post = cachedPost.HasValue ? JsonSerializer.Deserialize<Post>(cachedPost!) : null;

            return post;
        }

        public void Update(Post entity)
        {
            postRepository.Update(entity);

            if (cacheRepository.KeyExists(postKey))
            {
                entity.LastModifiedDate = DateTime.Now;
                var jsonPost = JsonSerializer.Serialize(entity);
                cacheRepository.HashSet(postKey, entity.Id.ToString(), jsonPost);
            }
        }

        public IQueryable<Post> Where(Expression<Func<Post, bool>> predicate)
        {
            if (!cacheRepository.KeyExists(postKey)) return FetchAndCacheEntities().AsQueryable().Where(predicate);

            var cachedPosts = cacheRepository.HashGetAll(postKey);
            if (cachedPosts.Length == 0) return Enumerable.Empty<Post>().AsQueryable();

            var posts = cachedPosts.Select(cachedPost => JsonSerializer.Deserialize<Post>(cachedPost.Value!)).ToList();
            return posts.AsQueryable().Where(predicate!)!;
        }

        private List<Post> FetchAndCacheEntities()
        {
            var posts = postRepository.GetAll().ToList();

            foreach (var post in posts)
            {
                var jsonPost = JsonSerializer.Serialize(post);
                cacheRepository.HashSet(postKey, post.Id.ToString(), jsonPost);
            }

            return posts;
        }

        private async Task<List<Post>> FetchAndCacheEntitiesAsync()
        {
            var posts = await postRepository.GetAll().ToListAsync();

            foreach (var post in posts)
            {
                var jsonPost = JsonSerializer.Serialize(post);
                await cacheRepository.HashSetAsync(postKey, post.Id.ToString(), jsonPost);
            }

            return posts;
        }
    }
}
