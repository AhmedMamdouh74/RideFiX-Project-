using RideFix.ErrorModels;
using Service.Exception_Implementation.AlreadyFound;

namespace RideFix.CustomMiddlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;
        public CustomExceptionMiddleware(ILogger<CustomExceptionMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                await NotFoundResourceHandler(context);
            }
            catch (Exception ex)
            {
                await ExceptionsHandler(context, ex);
            }
        }

        private async Task ExceptionsHandler(HttpContext context, Exception ex)
        {
            switch (ex)
            {
                case Domain.Exceptions.NotFoundException notFoundException:
                    _logger.LogError(ex, "Not Found Exception: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status404NotFound;
                    break;
                case Domain.Exceptions.BadRequestException badRequestException:
                    _logger.LogError(ex, "BadRequest Exception: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                case RequestAlreadyFoundException alreadyFoundException:
                    _logger.LogError(ex, "Request Already Found Exception: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status409Conflict;
                    break;
                default:
                    _logger.LogError(ex, "An unexpected error occurred: {Message}", ex.Message);
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }
            var response = new ErrorModel()
            {
                statusCode = context.Response.StatusCode,
                message = ex.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }

        private async Task NotFoundResourceHandler(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                _logger.LogWarning("404 Not Found: {Path}", context.Request.Path);
                var response = new ErrorModel()
                {
                    statusCode = context.Response.StatusCode,
                    message = $"Resource {context.Request.Path} not found."
                };
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
