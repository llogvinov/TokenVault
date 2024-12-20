using Microsoft.AspNetCore.Diagnostics;

namespace TokenVault.Api.RequestPipeline;

public static class WebApplicationExceptions
{
    public static WebApplication UseGlobalErrorHandling(this WebApplication app)
    {
        app.UseExceptionHandler("/error");
        app.MapGet("/error", (HttpContext httpContext) =>
        {
            Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (exception is not null)
            {
                return Results.Problem();
            }

            return exception switch
            {
                _ => Results.Problem()
            };
        });

        return app;
    }
}

