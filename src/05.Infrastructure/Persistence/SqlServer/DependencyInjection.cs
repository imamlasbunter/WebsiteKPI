using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer;
public static class DependencyInjection
{
    public static IServiceCollection AddSqlServerPersistenceService(this IServiceCollection services, SqlServerOptions sqlServerOptions, IHealthChecksBuilder healthChecksBuilder)
    {
        var migrationsAssembly = typeof(SqlServerWebsite_KPIDbContext).Assembly.FullName;

        services.AddDbContext<SqlServerWebsite_KPIDbContext>(options =>
        {
            options.UseSqlServer(sqlServerOptions.ConnectionString, builder =>
            {
                builder.MigrationsAssembly(migrationsAssembly);
                builder.MigrationsHistoryTable(TableNameFor.EfMigrationsHistory, nameof(Website_KPI));
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        services.AddScoped<IWebsite_KPIDbContext>(provider => provider.GetRequiredService<SqlServerWebsite_KPIDbContext>());

        healthChecksBuilder.AddSqlServer(
            connectionString: sqlServerOptions.ConnectionString,
            name: $"{nameof(Persistence)} {nameof(PersistenceOptions.Provider)} ({nameof(SqlServer)})");

        return services;
    }
}
