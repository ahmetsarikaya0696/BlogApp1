using Application;
using System.Net;

namespace WebApi.Extensions
{
    public static class EndpointResultExtension
    {
        public static IResult ToEndpointResult<T>(this ServiceResult<T> result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),
                HttpStatusCode.Created => Results.Created(result.UrlAsCreated, result.Data),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };
        }

        public static IResult ToEndpointResult(this ServiceResult result)
        {
            return result.StatusCode switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.Fail!),
                _ => Results.Problem(result.Fail!)
            };
        }
    }
}
