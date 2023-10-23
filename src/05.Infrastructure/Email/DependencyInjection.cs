﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Email;
using Pertamina.Website_KPI.Infrastructure.Email.EmailBlast;
using Pertamina.Website_KPI.Infrastructure.Email.None;
using Pertamina.Website_KPI.Infrastructure.Email.Smtp;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Email;
public static class DependencyInjection
{
    public static IServiceCollection AddEmailService(this IServiceCollection services, IConfiguration configuration, IHealthChecksBuilder healthChecksBuilder)
    {
        services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionKey));

        var emailOptions = configuration.GetSection(EmailOptions.SectionKey).Get<EmailOptions>();
        var fluentEmailServicesBuilder = services.AddFluentEmail(emailOptions.SenderEmailAddress);

        switch (emailOptions.Provider)
        {
            case EmailProvider.None:
                services.AddNoneEmailService();
                break;
            case EmailProvider.Smtp:
                services.Configure<SmtpEmailOptions>(configuration.GetSection(SmtpEmailOptions.SectionKey));
                var smtpOptions = configuration.GetSection(SmtpEmailOptions.SectionKey).Get<SmtpEmailOptions>();
                fluentEmailServicesBuilder.AddSmtpEmailService(smtpOptions, healthChecksBuilder);
                break;
            case EmailProvider.EmailBlast:
                services.Configure<EmailBlastEmailOptions>(configuration.GetSection(EmailBlastEmailOptions.SectionKey));
                var emailBlastEmailOptions = configuration.GetSection(EmailBlastEmailOptions.SectionKey).Get<EmailBlastEmailOptions>();
                fluentEmailServicesBuilder.AddEmailBlastEmailService(emailBlastEmailOptions, healthChecksBuilder);
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {nameof(Email)} {nameof(EmailOptions.Provider)}: {emailOptions.Provider}");
        }

        services.AddTransient<IEmailService, EmailService>();

        return services;
    }
}
