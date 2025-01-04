using WebApi.Filters;
using WebApi.Posts.Endpoints;

namespace WebApi.Posts
{
    public static class PostEndpointsExtension
    {
        public static void AddPostEndpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/posts")
               .WithTags("Posts")
               .CreatePostEndpointExtension()
               .GetPostByIdEndpointExtension()
               .UpdatePostEndpointExtension()
               .DeletePostEndpointExtension()
               .ViewCountIncrementEndpointExtension()
               .PostLikeChangeEndpointExtension()
               .AddEndpointFilter<FluentValidationFilter>();
        }
    }
}
