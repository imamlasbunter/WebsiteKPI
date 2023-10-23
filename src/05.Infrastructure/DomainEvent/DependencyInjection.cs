using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.DomainEvent;

namespace Pertamina.Website_KPI.Infrastructure.DomainEvent;
public static class DependencyInjection
{
    public static IServiceCollection AddDomainEventService(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
