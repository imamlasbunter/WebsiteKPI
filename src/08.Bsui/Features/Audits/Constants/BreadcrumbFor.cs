using MudBlazor;
using Pertamina.Website_KPI.Shared.Audits.Constants;

namespace Pertamina.Website_KPI.Bsui.Features.Audits.Constants;
public static class BreadcrumbFor
{
    public static readonly BreadcrumbItem Index = new(DisplayTextFor.Audits, href: RouteFor.Index);
}
