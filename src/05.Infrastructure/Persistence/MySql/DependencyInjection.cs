using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Persistence.MySql;
public static class DependencyInjection
{
    public static IServiceCollection AddMySqlPersistenceService(this IServiceCollection services, MySqlOptions mySqlOptions, IHealthChecksBuilder healthChecksBuilder)
    {
        var migrationsAssembly = typeof(MySqlWebsite_KPIDbContext).Assembly.FullName;

        services.AddDbContext<MySqlWebsite_KPIDbContext>(options =>
        {
            options.UseMySql(mySqlOptions.ConnectionString, ServerVersion.AutoDetect(mySqlOptions.ConnectionString), builder =>
            {
                builder.MigrationsAssembly(migrationsAssembly);
                builder.MigrationsHistoryTable(TableNameFor.EfMigrationsHistory);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            });

            options.ConfigureWarnings(wcb => wcb.Ignore(CoreEventId.RowLimitingOperationWithoutOrderByWarning));
            options.ConfigureWarnings(wcb => wcb.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
        });

        services.AddScoped<IWebsite_KPIDbContext>(provider => provider.GetRequiredService<MySqlWebsite_KPIDbContext>());

        healthChecksBuilder.AddMySql(
            connectionString: mySqlOptions.ConnectionString,
            name: $"{nameof(Persistence)} {nameof(PersistenceOptions.Provider)} ({nameof(MySql)})");

        return services;
    }
}
