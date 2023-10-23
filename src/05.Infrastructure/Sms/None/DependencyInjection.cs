using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Sms;

namespace Pertamina.Website_KPI.Infrastructure.Sms.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneSmsService(this IServiceCollection services)
    {
        services.AddSingleton<ISmsService, NoneSmsService>();

        return services;
    }
}
