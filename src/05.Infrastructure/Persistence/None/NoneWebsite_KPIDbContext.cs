using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Infrastructure.Persistence.None;
public class NoneWebsite_KPIDbContext : DbContext, IWebsite_KPIDbContext
{
    private readonly ILogger<NoneWebsite_KPIDbContext> _logger;

    public NoneWebsite_KPIDbContext(ILogger<NoneWebsite_KPIDbContext> logger)
    {
        _logger = logger;
    }

    #region Essential Entities
    public DbSet<Audit> Audits => Set<Audit>();
    #endregion Essential Entities

    #region Business Entities
    #endregion Business Entities

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is not implemented.", $"{nameof(Persistence)} {CommonDisplayTextFor.Service}");
    }

    public Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken) where THandler : notnull
    {
        LogWarning();

        return Task.FromResult(0);
    }
}
