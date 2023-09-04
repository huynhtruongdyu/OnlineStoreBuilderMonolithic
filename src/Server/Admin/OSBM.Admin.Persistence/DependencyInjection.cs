using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using OSBM.Admin.Application.Contracts.DbContexts;
using OSBM.Admin.Persistence.DbContexts;

namespace OSBM.Admin.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(opts =>
        {
            opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                });
        });
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        return services;
    }
}