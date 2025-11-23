using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using OrderService.Core.Models;
using OrderService.SharedKernel;
using Serilog;

namespace OrderService.Framework.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            Log.Error(ex, ex.Message);

            var responseError = Error.Failure("server.internal", ex.Message);

            var envelope = Envelope.Error(responseError.ToErrorList());

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(envelope);
        }
    }
}

public static class ExceptionMiddlewareExtensions
{
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ExceptionMiddleware>();
    }
}