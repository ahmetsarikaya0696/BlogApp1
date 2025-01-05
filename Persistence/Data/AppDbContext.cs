using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistence.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole, string>(options)
    {
        public required DbSet<Post> Posts { get; set; }
        public required DbSet<Tag> Tags { get; set; }
        public required DbSet<PostTag> PostTags { get; set; }
        public required DbSet<RefreshToken> RefreshTokens { get; set; }
        public required DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }

}
