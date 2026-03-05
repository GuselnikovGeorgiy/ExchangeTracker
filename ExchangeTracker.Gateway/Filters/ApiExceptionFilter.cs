using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ExchangeTracker.Gateway.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ApiExceptionFilter(ILogger<ApiExceptionFilter> logger) : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        logger.LogError(context.Exception, "Handling request error");

        var statusCode = GenerateStatusCode(context.Exception);
        
        context.HttpContext.Response.ContentType = "application/json";
        context.HttpContext.Response.StatusCode = (int)statusCode;

        context.Result = new JsonResult(new { error = context.Exception.GetBaseException().Message });

        base.OnException(context);
    }
    
    private static HttpStatusCode GenerateStatusCode(Exception exception) =>
        exception switch
        {
            // Common HTTP errors
            _ when exception is ArgumentException => HttpStatusCode.BadRequest,
            _ when exception is UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            _ when exception is FileNotFoundException => HttpStatusCode.NotFound,
            _ when exception is TimeoutException => HttpStatusCode.RequestTimeout,
            _ when exception is InvalidOperationException => HttpStatusCode.BadRequest, 

            // Database errors
            _ when exception is DbUpdateException => HttpStatusCode.Conflict,
            
            _ => HttpStatusCode.InternalServerError
        };
}