using Pertamina.Website_KPI.Bsui.Common.Constants;
using Pertamina.Website_KPI.Bsui.Pages.Account.Constants;
using Pertamina.Website_KPI.Bsui.Services.Authorization.Components;
using Pertamina.Website_KPI.Shared.Services.Authentication.Constants;

namespace Pertamina.Website_KPI.Bsui.Services.Authentication.Components;
public partial class AccountInfo
{
    private void NavigateToMySession()
    {
        _navigationManager.NavigateTo(CommonRouteFor.MySession, true);
    }

    private async Task ShowDialogSwitchPosition()
    {
        var dialog = _dialogService.Show<DialogSwitchPosition>(AuthenticationDisplayTextFor.SwitchPosition);
        var result = await dialog.Result;

        if (!result.Cancelled)
        {
            StateHasChanged();
        }
    }

    private void NavigateToLogout()
    {
        _navigationManager.NavigateTo(RouteFor.Logout, true);
    }
}
