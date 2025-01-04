using WebApi.Auth.Endpoints;
using WebApi.Search.Endpoints;

namespace WebApi.Search
{
    public static class SearchEndpointsExtension
    {
        public static void AddSearchEndpointsExtension(this WebApplication app)
        {
            app.MapGroup("api/search")
               .WithTags("Search")
               .SearchEndpointExtension();
        }
    }
}
