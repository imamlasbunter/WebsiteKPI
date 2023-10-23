using Pertamina.Website_KPI.Base.ValueObjects;

namespace Pertamina.Website_KPI.Client.Services.UserInfo;
public class UserInfoService
{
    public string AccessToken { get; set; } = default!;
    public string? PositionId { get; set; }
    public string? IpAddress { get; set; }
    public Geolocation? Geolocation { get; set; }
}
