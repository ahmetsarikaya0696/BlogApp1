using WebApi.Filters;
using WebApi.Tags.Endpoints;

namespace WebApi.Tags
{
    public static class TagEndpointsExtension
    {
        public static void AddTagEndpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/tags")
               .WithTags("Tags")
               .CreateTagEndpointExtension()
               .GetAllTagsEndpointExtension()
               .UpdateTagEndpointExtension()
               .DeleteTagEndpointExtension()
               .AddEndpointFilter<FluentValidationFilter>();
        }
    }
}
