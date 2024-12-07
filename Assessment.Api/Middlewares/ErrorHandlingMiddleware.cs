using Model.Responses;
using System.Net;
using System.Text.Json;
using static Core.Exceptions.DomainException;
namespace Assessment.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                HttpResponse response = context.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case ResourceInvalidOperationException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    case ResourceNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    case ResourceUnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    case ResourceArgumentException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    case ResourceValiationException:
                        response.StatusCode = (int)HttpStatusCode.Accepted;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError("DDOT.MPS.Resource.API.Middlewares.ErrorHandlingMiddleware | Exception: {exception}", exception.ToString());
                const string defaultErrorMessage = "SERVERSIDE_ERROR_OCCURED";
                BaseResponse<string> exceptionResponse = new BaseResponse<string>(response.StatusCode == (int)HttpStatusCode.InternalServerError ? defaultErrorMessage : exception?.Message);
                JsonSerializerOptions serializeOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
                string result = JsonSerializer.Serialize(exceptionResponse, serializeOptions);
                await response.WriteAsync(result);
            }
        }
    }
}
