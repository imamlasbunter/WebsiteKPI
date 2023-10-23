﻿using Pertamina.Website_KPI.Infrastructure.Logging;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.WebApi.Services.Documentation.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneDocumentationService(this IServiceCollection services)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Documentation)} {CommonDisplayTextFor.Service}");

        return services;
    }

    public static IApplicationBuilder UseNoneDocumentationService(this IApplicationBuilder app)
    {
        LoggingHelper
            .CreateLogger()
            .LogWarning("{ServiceName} is set to None.", $"{nameof(Documentation)} {CommonDisplayTextFor.Service}");

        return app;
    }
}
