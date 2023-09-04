using OSBM.Admin.Application;
using OSBM.Admin.Infrastructure;
using OSBM.Admin.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    //Add Application services
    builder.Services.ConfigureApplicationServices();

    //Add Infrastructure services
    builder.Services.ConfigureInfrastructureServices();

    //Add Persistence services
    builder.Services.ConfigurePersistenceServices(builder.Configuration);
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

app.Run();