using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.AppInfo;
using Pertamina.Website_KPI.Infrastructure.Authentication;
using Pertamina.Website_KPI.Infrastructure.Authorization;
using Pertamina.Website_KPI.Infrastructure.BackgroundJob;
using Pertamina.Website_KPI.Infrastructure.CurrentUser;
using Pertamina.Website_KPI.Infrastructure.DateAndTime;
using Pertamina.Website_KPI.Infrastructure.DomainEvent;
using Pertamina.Website_KPI.Infrastructure.Ecm;
using Pertamina.Website_KPI.Infrastructure.Email;
using Pertamina.Website_KPI.Infrastructure.HealthCheck;
using Pertamina.Website_KPI.Infrastructure.Otp;
using Pertamina.Website_KPI.Infrastructure.Persistence;
using Pertamina.Website_KPI.Infrastructure.Sms;
using Pertamina.Website_KPI.Infrastructure.Storage;
using Pertamina.Website_KPI.Infrastructure.Telemetry;
using Pertamina.Website_KPI.Infrastructure.UserProfile;

namespace Pertamina.Website_KPI.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        #region Health Check
        var healthChecksBuilder = services.AddHealthCheckService(configuration);
        #endregion Health Check

        #region AppInfo
        services.AddAppInfoService(configuration);
        #endregion AppInfo

        #region Authentication
        services.AddAuthenticationService(configuration, healthChecksBuilder);
        #endregion Authentication

        #region Authorization
        services.AddAuthorizationService(configuration, healthChecksBuilder);
        #endregion Authorization

        #region Background Job
        services.AddBackgroundJobService(configuration, healthChecksBuilder);
        #endregion Background Job

        #region Current User
        services.AddCurrentUserService();
        #endregion Current User

        #region DateTime
        services.AddDateAndTimeService();
        #endregion DateTime

        #region Domain Event
        services.AddDomainEventService();
        #endregion Domain Event

        #region Enterprise Content Management
        services.AddEcmService(configuration, healthChecksBuilder);
        #endregion Enterprise Content Management

        #region Email
        services.AddEmailService(configuration, healthChecksBuilder);
        #endregion Email

        #region One Time Password
        services.AddOtpService();
        #endregion One Time Password

        #region Persistence
        services.AddPersistenceService(configuration, healthChecksBuilder);
        #endregion Persistence

        #region SMS
        services.AddSmsService(configuration, healthChecksBuilder);
        #endregion SMS

        #region Storage
        services.AddStorageService(configuration, healthChecksBuilder);
        #endregion Storage

        #region Telemetry
        services.AddTelemetryService(configuration);
        #endregion Telemetry

        #region User Profile
        services.AddUserProfileService(configuration, healthChecksBuilder);
        #endregion User Profile

        return services;
    }

    public static IApplicationBuilder UseInfrastructure(this IApplicationBuilder app, IConfiguration configuration)
    {
        #region Health Check
        app.UseHealthCheckService(configuration);
        #endregion Health Check

        #region Background Job
        app.UseBackgroundJobService(configuration);
        #endregion Background Job

        return app;
    }
}
