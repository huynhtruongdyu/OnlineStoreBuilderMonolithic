using Microsoft.EntityFrameworkCore;

namespace OSBM.Admin.API.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetService<T>();
        context?.Database.Migrate();
    }
}