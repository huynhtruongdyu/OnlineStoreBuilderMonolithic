using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using OSBM.Admin.API.Extensions;
using OSBM.Admin.API.Middlewares;
using OSBM.Admin.Application;
using OSBM.Admin.Domain.Identities;
using OSBM.Admin.Infrastructure;
using OSBM.Admin.Persistence;
using OSBM.Admin.Persistence.DbContexts;
using OSBM.Admin.Shared.Models.ApiResponse;

using System.Net.Mime;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });

    builder.Services.AddResponseCaching();

    #region Add Service layers

    //Add Application services
    builder.Services.ConfigureApplicationServices();

    //Add Infrastructure services
    builder.Services.ConfigureInfrastructureServices();

    //Add Persistence services
    builder.Services.ConfigurePersistenceServices(builder.Configuration);

    #endregion Add Service layers

    //Configure API Behavior
    builder.Services.ConfigureApiBehavior();

    //Configure CORS Policy
    builder.Services.ConfigureCorsPolicy();

    //Configure API Versioning
    builder.Services.ConfigureApiVersioning();

    //Configure ApiExplorer to discover versions
    builder.Services.ConfigureApiExplorerToDiscoverVersions();

    ///Configure Rate Limiter
    ///refer:
    ///- https://www.youtube.com/watch?v=1tPVVDEDGtE
    ///- https://blog.maartenballiauw.be/post/2022/09/26/aspnet-core-rate-limiting-middleware.html
    builder.Services.ConfigureRateLimiter();

    //Configure Authenticate
    builder.Services.AddIdentity<AppUser, AppRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                ValidAudience = builder.Configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])),

                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
            x.Events = new JwtBearerEvents
            {
                OnChallenge = async context =>
                {
                    // Call this to skip the default logic and avoid using the default response
                    context.HandleResponse();

                    // Write to the response in any way you wish
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    await context.Response.WriteAsync(new ApiReponseModel(false, StatusCodes.Status401Unauthorized, messages: "unauthorize").ToString());
                }
            };
        });
    builder.Services.AddAuthorization();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }

    app.EnsureMigrationOfContext<ApplicationDbContext>();

    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseRateLimiter();

    app.UseResponseCaching();

    app.UseAuthorization();

    //Configure Error Handling
    //app.UseErrorHandler(app.Environment);
    app.UseMiddleware<ErrorHandlingMiddleware>();

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "wwwroot")),
        RequestPath = "/public",
        OnPrepareResponse = ctx =>
        {
            // Set cache control headers
            ctx.Context.Response.Headers.Append("Cache-Control", "public, max-age=3600"); // Cache for 1 hour
        }
    }); ;

    app.UseCors();

    app.MapControllers();

    app.Run();
}