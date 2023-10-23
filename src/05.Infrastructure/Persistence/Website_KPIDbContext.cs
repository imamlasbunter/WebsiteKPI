﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Pertamina.Website_KPI.Application.Services.CurrentUser;
using Pertamina.Website_KPI.Application.Services.DateAndTime;
using Pertamina.Website_KPI.Application.Services.DomainEvent;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Domain.Events;
using Pertamina.Website_KPI.Domain.Interfaces;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Extensions;
using Pertamina.Website_KPI.Infrastructure.Persistence.Common.Models;

namespace Pertamina.Website_KPI.Infrastructure.Persistence;
public class Website_KPIDbContext : DbContext, IWebsite_KPIDbContext
{
    private const string SoftDeleted = nameof(SoftDeleted);

    protected ICurrentUserService _currentUser = default!;
    protected IDateAndTimeService _dateTime = default!;
    protected IDomainEventService _domainEvent = default!;

    #region Essential Entities
    public DbSet<Audit> Audits => Set<Audit>();
    #endregion Essential Entities

    #region Business Entities
    #endregion Business Entities

    public Website_KPIDbContext(
        DbContextOptions<Website_KPIDbContext> options,
        ICurrentUserService currentUser,
        IDateAndTimeService dateTime,
        IDomainEventService domainEvent) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;
        _domainEvent = domainEvent;
    }

    protected Website_KPIDbContext(DbContextOptions options)
        : base(options)
    {
    }

    public async Task<int> SaveChangesAsync<THandler>(THandler handler, CancellationToken cancellationToken = default) where THandler : notnull
    {
        foreach (var entry in ChangeTracker.Entries<ICreatable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUser.Username;
                    entry.Entity.Created = _dateTime.Now;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<IModifiable>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.ModifiedBy = _currentUser.Username;
                    entry.Entity.Modified = _dateTime.Now;
                    break;
            }
        }

        var auditEntries = OnBeforeSaveChanges(handler.GetType().Name);
        var result = await base.SaveChangesAsync(true, cancellationToken);

        await OnAfterSaveChanges(auditEntries);
        await DispatchEvents();

        return result;
    }

    private List<AuditEntry> OnBeforeSaveChanges(string actionName)
    {
        ChangeTracker.DetectChanges();

        var auditEntries = new List<AuditEntry>();

        foreach (var entry in ChangeTracker.Entries())
        {
            var isAuditableEntity = entry.Entity.GetType().IsAuditableEntity();

            if (!isAuditableEntity || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
            {
                continue;
            }

            var entityType = Model.FindEntityType(entry.Entity.GetType());

            if (entityType is null)
            {
                throw new InvalidOperationException($"Cannot find Entity Type for {entry.Entity.GetType()}");
            }

            var tableName = $"{entityType.GetSchema()}.{entityType.GetTableName()}";
            var entityName = entry.Entity.GetType().Name;

            var auditEntry = new AuditEntry
            {
                TableName = tableName,
                EntityName = entityName,
                ClientApplicationId = _currentUser.ClientId,
                CreatedBy = _currentUser.Username,
                FromIpAddress = _currentUser.IpAddress,
                FromGeolocation = _currentUser.Geolocation,
                ActionName = actionName,
                ActionType = entry.State.ToString()
            };

            auditEntries.Add(auditEntry);

            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    // The value will be generated by the database, get the value after saving.
                    auditEntry.TemporaryProperties.Add(property);
                    continue;
                }

                var propertyName = property.Metadata.Name;
                var originalValue = property.OriginalValue;
                var currentValue = property.CurrentValue;

                if (property.Metadata.IsPrimaryKey())
                {
                    if (currentValue is null)
                    {
                        continue;
                    }

                    if (propertyName == nameof(IHasKey.Id))
                    {
                        var entityId = currentValue.ToString();

                        if (!string.IsNullOrWhiteSpace(entityId))
                        {
                            auditEntry.EntityId = new Guid(entityId);
                        }
                    }

                    continue;
                }

                switch (entry.State)
                {
                    case EntityState.Added:
                        if (propertyName is nameof(IDeletable.IsDeleted) or nameof(IModifiable.Modified) or nameof(IModifiable.ModifiedBy))
                        {
                            continue;
                        }

                        auditEntry.NewValues[propertyName] = currentValue;
                        break;
                    case EntityState.Deleted:
                        auditEntry.OldValues[propertyName] = originalValue;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.OldValues[propertyName] = originalValue;
                            auditEntry.NewValues[propertyName] = currentValue;

                            if (propertyName is nameof(IDeletable.IsDeleted) && (bool)currentValue! is true)
                            {
                                auditEntry.ActionType = SoftDeleted;
                            }
                        }

                        break;
                }
            }
        }

        // Save audit entities that have all the modifications.
        foreach (var auditEntry in auditEntries.Where(x => !x.HasTemporaryProperties))
        {
            Audits.Add(auditEntry.ToAudit());
        }

        // Keep a list of entries where the value of some properties are unknown at this step.
        return auditEntries.Where(x => x.HasTemporaryProperties).ToList();
    }

    private Task OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        if (auditEntries is null || auditEntries.Count == 0)
        {
            return Task.CompletedTask;
        }

        foreach (var auditEntry in auditEntries)
        {
            // Get the final value of the temporary properties.
            foreach (var property in auditEntry.TemporaryProperties)
            {
                var currentValue = property.CurrentValue;
                var propertyName = property.Metadata.Name;

                if (currentValue is null)
                {
                    continue;
                }

                if (property.Metadata.IsPrimaryKey())
                {
                    if (propertyName == nameof(IHasKey.Id))
                    {
                        var entityId = currentValue.ToString();

                        if (!string.IsNullOrWhiteSpace(entityId))
                        {
                            auditEntry.EntityId = new Guid(entityId);
                        }
                    }
                }
                else
                {
                    auditEntry.NewValues[propertyName] = currentValue;
                }
            }

            Audits.Add(auditEntry.ToAudit());
        }

        return SaveChangesAsync();
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(domainEvent => !domainEvent.IsPublished)
                .FirstOrDefault();

            if (domainEventEntity is null)
            {
                break;
            }

            domainEventEntity.IsPublished = true;

            await _domainEvent.Publish(domainEventEntity);
        }
    }
}
