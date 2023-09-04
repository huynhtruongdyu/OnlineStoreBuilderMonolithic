using Microsoft.Extensions.DependencyInjection;

using OSBM.Admin.Application.Contracts.Services;
using OSBM.Admin.Infrastructure.Services;

namespace OSBM.Admin.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        return services;
    }
}