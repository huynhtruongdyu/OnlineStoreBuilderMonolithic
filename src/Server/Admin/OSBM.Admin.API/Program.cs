using Microsoft.EntityFrameworkCore;

using OSBM.Admin.API.Extensions;
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
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Add Application services
    builder.Services.ConfigureApplicationServices();

    //Add Infrastructure services
    builder.Services.ConfigureInfrastructureServices();

    //Add Persistence services
    builder.Services.ConfigurePersistenceServices(builder.Configuration);

    //Configure Api Behavior
    builder.Services.ConfigureApiBehavior();

    //Configure CORS Policy
    builder.Services.ConfigureCorsPolicy();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.EnsureMigrationOfContext<ApplicationDbContext>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.UseErrorHandler(app.Environment);

    app.UseCors();

    app.MapControllers();
}

app.Run();