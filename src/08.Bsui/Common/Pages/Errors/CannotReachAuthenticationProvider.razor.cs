using Pertamina.Website_KPI.Bsui.Services.Authentication;
using Pertamina.Website_KPI.Bsui.Services.Authentication.IdAMan;

namespace Pertamina.Website_KPI.Bsui.Common.Pages.Errors;
public partial class CannotReachAuthenticationProvider
{
    private string _authenticationProviderUrl = default!;

    protected override void OnInitialized()
    {
        switch (_authenticationOptions.Value.Provider)
        {
            case AuthenticationProvider.None:
                break;
            case AuthenticationProvider.IdAMan:
                var idAManAuthenticationOptions = configuration.GetSection(IdAManAuthenticationOptions.SectionKey).Get<IdAManAuthenticationOptions>();
                _authenticationProviderUrl = idAManAuthenticationOptions.AuthorityUrl;
                break;
            default:
                break;
        }
    }
}
