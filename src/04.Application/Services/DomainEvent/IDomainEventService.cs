namespace Pertamina.Website_KPI.Application.Services.DomainEvent;

public interface IDomainEventService
{
    Task Publish(Domain.Events.DomainEvent domainEvent);
}
