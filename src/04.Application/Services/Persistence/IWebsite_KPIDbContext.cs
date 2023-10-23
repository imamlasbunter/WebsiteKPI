using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Domain.Entities;

namespace Pertamina.Website_KPI.Application.Services.Persistence;
public interface IWebsite_KPIDbContext
{
    #region Essential Entities
    DbSet<Audit> Audits { get; }
    #endregion Essential Entities

    #region Business Entities
    #endregion Business Entities

    Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull;
}
