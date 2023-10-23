﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Infrastructure.Authentication.IdAMan;
using Pertamina.Website_KPI.Infrastructure.Authentication.IS4IM;
using Pertamina.Website_KPI.Infrastructure.Authentication.None;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Authentication;
public static class DependencyInjection
{
    public static IServiceCollection AddAuthenticationService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.SectionKey));

        var authenticationOptions = configuration.GetSection(AuthenticationOptions.SectionKey).Get<AuthenticationOptions>();

        switch (authenticationOptions.Provider)
        {
            case AuthenticationProvider.None:
                services.AddNoneAuthenticationService();
                break;
            case AuthenticationProvider.IdAMan:
                services.AddIdAManAuthenticationService(configuration, healthChecksBuilder);
                break;
            case AuthenticationProvider.IS4IM:
                services.AddIS4IMAuthenticationService(configuration, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Authentication)} {nameof(AuthenticationOptions.Provider)}: {authenticationOptions.Provider}");
        }

        return services;
    }
}
