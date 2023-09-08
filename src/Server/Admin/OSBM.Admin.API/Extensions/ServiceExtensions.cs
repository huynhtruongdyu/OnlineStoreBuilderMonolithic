using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.RateLimiting;

using OSBM.Admin.API.Options;
using OSBM.Admin.Shared.Models.ApiResponse;

using System.Threading.RateLimiting;

namespace OSBM.Admin.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureApiBehavior(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });
    }

    public static void ConfigureCorsPolicy(this IServiceCollection services)
    {
        services.AddCors(opt =>
        {
            opt.AddDefaultPolicy(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        });
    }

    public static void ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(1, 0);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
            config.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                            new HeaderApiVersionReader("x-api-version"),
                                                            new MediaTypeApiVersionReader("x-api-version"));
        });
    }

    public static void ConfigureApiExplorerToDiscoverVersions(this IServiceCollection services)
    {
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.ConfigureOptions<ConfigureSwaggerOptions>();
    }

    public static void ConfigureRateLimiter(this IServiceCollection services)
    {
        services.AddRateLimiter(rateLimitOpts =>
        {
            rateLimitOpts.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.HttpContext.Response.ContentType = "application/json";
                await context.HttpContext.Response.WriteAsync(new ApiReponseModel(false, StatusCodes.Status429TooManyRequests, messages: "too.many.request").ToString());
                //if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
                //{
                //    await context.HttpContext.Response.WriteAsync(
                //        $"Too many requests. Please try again after {retryAfter.TotalMinutes} minute(s). " +
                //        $"Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
                //}
                //else
                //{
                //    await context.HttpContext.Response.WriteAsync(
                //        "Too many requests. Please try again later. " +
                //        "Read more about our rate limits at https://example.org/docs/ratelimiting.", cancellationToken: token);
                //}
            };

            rateLimitOpts.AddFixedWindowLimiter("fixed", opts =>
            {
                // 3 req / 10s
                opts.Window = TimeSpan.FromSeconds(10);
                opts.PermitLimit = 100;

                opts.QueueLimit = 10;
                opts.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });
    }
}