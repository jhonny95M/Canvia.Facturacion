using Canvia.Facturacion.Application.Commons;
using System.Net;
using System.Text.Json;

namespace Canvia.Facturacion.Api.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            BaseResponse<string> problem = new BaseResponse<string>
            {
                IsSucces=false,
                Data="Server error",
                Errors=null,
                Message="Server error"
            };
            string json = JsonSerializer.Serialize(problem);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(json);
        }
    }
}
