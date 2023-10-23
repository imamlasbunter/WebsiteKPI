﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.BackgroundJob.Hangfire;
using Pertamina.Website_KPI.Infrastructure.BackgroundJob.None;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Extensions;

namespace Pertamina.Website_KPI.Infrastructure.BackgroundJob;
public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        var backgroundJobOptions = configuration.GetSection(BackgroundJobOptions.SectionKey).Get<BackgroundJobOptions>();

        switch (backgroundJobOptions.Provider)
        {
            case BackgroundJobProvider.None:
                services.AddNoneBackgroundJobService();
                break;
            case BackgroundJobProvider.Hangfire:
                services.AddHangfireBackgroundJobService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(BackgroundJob).SplitWords()} {nameof(BackgroundJobOptions.Provider)}: {backgroundJobOptions.Provider}");
        }

        return services;
    }

    public static IApplicationBuilder UseBackgroundJobService(this IApplicationBuilder app, IConfiguration configuration)
    {
        var backgroundJobOptions = configuration.GetSection(BackgroundJobOptions.SectionKey).Get<BackgroundJobOptions>();

        switch (backgroundJobOptions.Provider)
        {
            case BackgroundJobProvider.None:
                break;
            case BackgroundJobProvider.Hangfire:
                app.UseHangfireBackgroundJobService(configuration);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(BackgroundJob).SplitWords()} {nameof(BackgroundJobOptions.Provider)}: {backgroundJobOptions.Provider}");
        }

        return app;
    }
}
