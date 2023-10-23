using Pertamina.Website_KPI.Bsui.Services.AppInfo;
using Pertamina.Website_KPI.Bsui.Services.Authentication;
using Pertamina.Website_KPI.Bsui.Services.Authorization;
using Pertamina.Website_KPI.Bsui.Services.External;
using Pertamina.Website_KPI.Bsui.Services.FrontEnd;
using Pertamina.Website_KPI.Bsui.Services.Geolocation;
using Pertamina.Website_KPI.Bsui.Services.Security;
using Pertamina.Website_KPI.Bsui.Services.Telemetry;
using Pertamina.Website_KPI.Bsui.Services.UI;

namespace Pertamina.Website_KPI.Bsui.Services;
public static class DependencyInjection
{
    public static IServiceCollection AddBsui(this IServiceCollection services, IConfiguration configuration)
    {
        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region Authentication
        services.AddAuthenticationService(configuration);
        #endregion Authentication

        #region Authorization
        services.AddAuthorizationService(configuration);
        #endregion Authorization

        #region External
        services.AddExternalService(configuration);
        #endregion External

        #region Front End
        services.AddFrontEndService(configuration);
        #endregion Front End

        #region Geolocation
        services.AddGeolocationService(configuration);
        #endregion Geolocation

        #region Security
        services.AddSecurityService();
        #endregion Security

        #region Telemetry
        services.AddTelemetryService(configuration);
        #endregion Telemetry

        #region User Interface
        services.AddUIService();
        #endregion User Interface

        return services;
    }

    public static IApplicationBuilder UseBsui(this IApplicationBuilder app, IConfiguration configuration)
    {
        app.UseFrontEndService(configuration);

        return app;
    }
}
