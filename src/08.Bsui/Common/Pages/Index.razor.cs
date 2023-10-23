using MudBlazor;
using Pertamina.Website_KPI.Bsui.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Extensions;

namespace Pertamina.Website_KPI.Bsui.Common.Pages;
public partial class Index
{
    private List<BreadcrumbItem> _breadcrumbItems = new();
    private string _greetings = default!;

    protected override void OnInitialized()
    {
        SetupBreadcrumb();

        _greetings = $"Good {DateTimeOffset.Now.ToFriendlyTimeDisplayText()}";
    }

    private void SetupBreadcrumb()
    {
        _breadcrumbItems = new()
        {
            CommonBreadcrumbFor.Active(CommonDisplayTextFor.Home)
        };
    }
}
