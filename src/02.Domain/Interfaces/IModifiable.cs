namespace Pertamina.Website_KPI.Domain.Interfaces;

public interface IModifiable
{
    DateTimeOffset? Modified { get; set; }
    string? ModifiedBy { get; set; }
}
