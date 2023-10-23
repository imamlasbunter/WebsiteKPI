using Microsoft.AspNetCore.Components;

namespace Pertamina.Website_KPI.Bsui.Services.Geolocation.Components;
public partial class GeolocationError
{
    [Parameter]
    public string ErrorMessage { get; set; } = default!;
}
