﻿using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Authorization;

namespace Pertamina.Website_KPI.Infrastructure.Authorization.None;
public static class DependencyInjection
{
    public static IServiceCollection AddNoneAuthorizationService(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationService, NoneAuthorizationService>();

        return services;
    }
}
