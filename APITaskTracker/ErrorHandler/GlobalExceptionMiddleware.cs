namespace APITaskTracker.ErrorHandler
{
    using System.Net;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseGlobalExceptionHandler(this WebApplication app, ILogger? logger = null)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    context.Response.ContentType = "application/problem+json";

                    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    var log = logger ?? app.Logger;
                    log.LogError(exception, "Unhandled exception at {Path}", context.Request.Path);


                    var problem = new ProblemDetails
                    {
                        Type = "https://localhost:44352/errors/internal-server-error",
                        Title = "An unexpected error occurred",
                        Status = (int)HttpStatusCode.InternalServerError,
                        Detail = exception?.Message
                    };

                    if (app.Environment.IsDevelopment())
                    {
                        problem.Extensions["stackTrace"] = exception?.StackTrace;
                    }

                    context.Response.StatusCode = problem.Status ?? 500;

                    await context.Response.WriteAsJsonAsync(problem);
                });
            });
        }
    }

}
