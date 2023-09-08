using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Shared.Models.ApiResponse;
using OSBM.Admin.Shared.Models.Exceptions;

namespace OSBM.Admin.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetService<T>();
        context?.Database.Migrate();
    }

    public static void UseErrorHandler(this IApplicationBuilder app, IHostEnvironment env)
    {
        app.UseExceptionHandler(appError =>
        {
            appError.Run(async context =>
            {
                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature == null) return;

                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                context.Response.ContentType = "application/json";

                context.Response.StatusCode = contextFeature.Error switch
                {
                    OSBBadRequestException => StatusCodes.Status400BadRequest,
                    OSBOperationCanceledException => StatusCodes.Status503ServiceUnavailable,
                    OSBNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                var errorResponse = new ApiReponseModel
                {
                    IsSuccess = false,
                    StatusCode = context.Response.StatusCode,
                    Messages = contextFeature.Error.GetBaseException().Message,
                    StackTrace = env.IsDevelopment() ? contextFeature.Error.StackTrace : null
                };

                await context.Response.WriteAsync(errorResponse.ToString());
            });
        });
    }
}