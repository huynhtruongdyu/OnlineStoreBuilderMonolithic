using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

using OSBM.Admin.Shared.Models.ApiResponse;
using OSBM.Admin.Shared.Models.Exceptions;

using System.Net;

using System.Text.Json;

using OperationCanceledException = OSBM.Admin.Shared.Models.Exceptions.OperationCanceledException;

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
                    BadRequestException => (int)HttpStatusCode.BadRequest,
                    OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                    NotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                var errorResponse = new ApiReponseModel
                {
                    StatusCode = context.Response.StatusCode,
                    Messages = contextFeature.Error.GetBaseException().Message,
                    IsSuccess = false,
                    StackTrace = env.IsDevelopment() ? contextFeature.Error.StackTrace : null
                };

                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            });
        });
    }
}