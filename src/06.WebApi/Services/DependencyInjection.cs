using Pertamina.Website_KPI.WebApi.Services.BackEnd;
using Pertamina.Website_KPI.WebApi.Services.Documentation;

namespace Pertamina.Website_KPI.WebApi.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddBackEndService(configuration);
        services.AddDocumentationService(configuration);

        return services;
    }

    public static IApplicationBuilder UseWebApi(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseDocumentationService(configuration);

        return app;
    }
}
