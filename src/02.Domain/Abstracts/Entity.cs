using Pertamina.Website_KPI.Domain.Interfaces;

namespace Pertamina.Website_KPI.Domain.Abstracts;
public abstract class Entity : IHasKey, ICreatable
{
    public Guid Id { get; set; }
    public DateTimeOffset Created { get; set; }
    public string CreatedBy { get; set; } = default!;
}
