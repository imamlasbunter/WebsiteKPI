using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Application.Services.CurrentUser;
using Pertamina.Website_KPI.Application.Services.DateAndTime;
using Pertamina.Website_KPI.Application.Services.DomainEvent;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Extensions;
using Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer.Configuration;

namespace Pertamina.Website_KPI.Infrastructure.Persistence.SqlServer;
public class SqlServerWebsite_KPIDbContext : Website_KPIDbContext
{
    public SqlServerWebsite_KPIDbContext(
        DbContextOptions<SqlServerWebsite_KPIDbContext> options,
        ICurrentUserService currentUser,
        IDateAndTimeService dateTime,
        IDomainEventService domainEvent) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
        _domainEvent = domainEvent;
    }

    public SqlServerWebsite_KPIDbContext(DbContextOptions<SqlServerWebsite_KPIDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromNameSpace(typeof(AuditConfiguration).Namespace!);

        base.OnModelCreating(builder);
    }
}
