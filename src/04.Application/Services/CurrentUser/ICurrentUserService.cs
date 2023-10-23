using Pertamina.Website_KPI.Base.ValueObjects;

namespace Pertamina.Website_KPI.Application.Services.CurrentUser;
public interface ICurrentUserService
{
    Guid? UserId { get; }
    string Username { get; }
    string ClientId { get; }
    string? PositionId { get; }
    string IpAddress { get; }
    Geolocation? Geolocation { get; }
}
