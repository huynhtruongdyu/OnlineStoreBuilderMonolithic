using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace OSBM.Admin.Persistence.DbContexts;

public class ApplicationDbContextFactory /*: IDesignTimeDbContextFactory<ApplicationDbContext>*/
{
    //public ApplicationDbContext CreateDbContext(string[] args)
    //{
    //    var configuration = new ConfigurationBuilder()
    //        .SetBasePath(Directory.GetCurrentDirectory())
    //        .AddJsonFile("appsettings.json")
    //        .Build();

    //    var dbContextBuilder = new DbContextOptionsBuilder<KuchidDbContext>();

    //    var connectionString = configuration.GetConnectionString("Kuchid");

    //    dbContextBuilder.UseSqlServer(connectionString);

    //    return new KuchidDbContext(dbContextBuilder.Options);
    //}
}