using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

using OSBM.Admin.API.Extensions;
using OSBM.Admin.API.Options;
using OSBM.Admin.Application;
using OSBM.Admin.Infrastructure;
using OSBM.Admin.Persistence;
using OSBM.Admin.Persistence.DbContexts;

using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
        });
    //builder.Services.AddEndpointsApiExplorer();
    //builder.Services.AddSwaggerGen();

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

    // Add ApiExplorer to discover versions
    builder.Services.ConfigureApiExplorerToDiscoverVersions();
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

    app.UseAuthorization();

    app.UseErrorHandler(app.Environment);

    app.UseCors();

    app.MapControllers();
}

app.Run();