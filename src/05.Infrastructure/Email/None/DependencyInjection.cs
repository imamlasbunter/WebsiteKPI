using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Email;

namespace Pertamina.Website_KPI.Infrastructure.Email.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneEmailService(this IServiceCollection services)
    {
        services.AddSingleton<IEmailService, NoneEmailService>();

        return services;
    }
}
