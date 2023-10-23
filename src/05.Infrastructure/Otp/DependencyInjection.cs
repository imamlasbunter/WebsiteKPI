using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Otp;

namespace Pertamina.Website_KPI.Infrastructure.Otp;
public static class DependencyInjection
{
    public static IServiceCollection AddOtpService(this IServiceCollection services)
    {
        services.AddTransient<IOtpService, OtpService>();

        return services;
    }
}
