using Refit;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Application
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        public ProblemDetails? Fail { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Fail is null;

        [JsonIgnore]
        public bool IsFail => !IsSuccess;

        public static ServiceResult SuccessAsNoContent() => new ServiceResult { StatusCode = HttpStatusCode.NoContent };

        public static ServiceResult ErrorAsNotFound()
        {
            return new ServiceResult()
            {
                StatusCode = HttpStatusCode.NotFound,
                Fail = new ProblemDetails() { Title = "Not Found", Detail = "The requested resource was not found" }
            };
        }
        public static ServiceResult ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };
        }
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }
        public static ServiceResult Error(string title, string detail, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode()
                }
            };
        }
        public static ServiceResult Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }
        public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data) => new ServiceResult<T> { StatusCode = HttpStatusCode.OK, Data = data };
        public static ServiceResult<T> SuccessAsCreated(T data, string url) => new ServiceResult<T> { StatusCode = HttpStatusCode.Created, Data = data, UrlAsCreated = url };
        public static new ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
        {
            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult<T>()
                {
                    Fail = new ProblemDetails()
                    {
                        Title = exception.Message
                    },
                    StatusCode = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

            return new ServiceResult<T>()
            {
                Fail = problemDetails,
                StatusCode = exception.StatusCode
            };
        }
        public static new ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = problemDetails
            };
        }
        public static new ServiceResult<T> Error(string title, string detail, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Detail = detail,
                    Status = statusCode.GetHashCode()
                }
            };
        }
        public static new ServiceResult<T> Error(string title, HttpStatusCode statusCode)
        {
            return new ServiceResult<T>()
            {
                StatusCode = statusCode,
                Fail = new ProblemDetails()
                {
                    Title = title,
                    Status = statusCode.GetHashCode()
                }
            };
        }
        public static new ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
        {
            return new ServiceResult<T>()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Fail = new ProblemDetails()
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = HttpStatusCode.BadRequest.GetHashCode()
                }
            };
        }
    }
}
