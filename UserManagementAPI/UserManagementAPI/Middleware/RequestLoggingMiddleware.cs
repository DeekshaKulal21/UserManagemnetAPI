namespace UserManagementAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log the request
            _logger.LogInformation($"HTTP {context.Request.Method} {context.Request.Path}");

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the response
            _logger.LogInformation($"Response {context.Response.StatusCode}");
        }
    }
}