using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Pertamina.Website_KPI.Bsui.Common.Pages.Errors.Constants;
using Pertamina.Website_KPI.Bsui.Services.Authentication;
using Pertamina.Website_KPI.Bsui.Services.Authentication.IdAMan;
using Pertamina.Website_KPI.Bsui.Services.Authentication.IS4IM;
using Pertamina.Website_KPI.Bsui.Services.Authorization;
using Pertamina.Website_KPI.Bsui.Services.Authorization.IdAMan;
using Pertamina.Website_KPI.Bsui.Services.Authorization.IS4IM;
using Pertamina.Website_KPI.Client.Services.HealthCheck;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Services.Authentication.Constants;
using Pertamina.Website_KPI.Shared.Services.Authorization.Constants;
using Pertamina.Website_KPI.Shared.Services.HealthCheck.Constants;

using AuthenticationOptions = Pertamina.Website_KPI.Bsui.Services.Authentication.AuthenticationOptions;

namespace Pertamina.Website_KPI.Bsui.Pages.Account;
public class LoginModel : PageModel
{
    private readonly HealthCheckService _healthCheckService;
    private readonly bool _usingAuthentication;
    private readonly string? _authenticationHealthCheckUrl;
    private readonly string? _authorizationHealthCheckUrl;

    public LoginModel(
        HealthCheckService healthCheckService,
        IOptions<AuthenticationOptions> authenticationOptions,
        IOptions<AuthorizationOptions> authorizationOptions,
        IConfiguration configuration)
    {
        _healthCheckService = healthCheckService;
        _usingAuthentication = authenticationOptions.Value.Provider is not AuthenticationProvider.None;

        switch (authenticationOptions.Value.Provider)
        {
            case AuthenticationProvider.None:
                _authenticationHealthCheckUrl = null;
                break;
            case AuthenticationProvider.IdAMan:
                var idAManAuthenticationOptions = configuration.GetSection(IdAManAuthenticationOptions.SectionKey).Get<IdAManAuthenticationOptions>();
                _authenticationHealthCheckUrl = idAManAuthenticationOptions.HealthCheckUrl;
                break;
            case AuthenticationProvider.IS4IM:
                var is4imAuthenticationOptions = configuration.GetSection(IS4IMAuthenticationOptions.SectionKey).Get<IS4IMAuthenticationOptions>();
                _authenticationHealthCheckUrl = is4imAuthenticationOptions.HealthCheckUrl;
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {AuthenticationDisplayTextFor.AuthenticationProvider}: {authenticationOptions.Value.Provider}");
        }

        switch (authorizationOptions.Value.Provider)
        {
            case AuthorizationProvider.None:
                _authenticationHealthCheckUrl = null;
                break;
            case AuthorizationProvider.IdAMan:
                var idAManAuthorizationOptions = configuration.GetSection(IdAManAuthorizationOptions.SectionKey).Get<IdAManAuthorizationOptions>();
                _authorizationHealthCheckUrl = idAManAuthorizationOptions.HealthCheckUrl;
                break;
            case AuthorizationProvider.IS4IM:
                var is4imAuthorizationOptions = configuration.GetSection(IS4IMAuthorizationOptions.SectionKey).Get<IS4IMAuthorizationOptions>();
                _authorizationHealthCheckUrl = is4imAuthorizationOptions.HealthCheckUrl;
                break;
            default:
                throw new ArgumentException($"{CommonDisplayTextFor.Unsupported} {AuthorizationDisplayTextFor.AuthorizationProvider}: {authorizationOptions.Value.Provider}");
        }
    }

    public async Task<IActionResult> OnGetAsync(string returnUrl)
    {
        if (!_usingAuthentication)
        {
            Response.Redirect(returnUrl);
        }

        if (!string.IsNullOrWhiteSpace(_authenticationHealthCheckUrl))
        {
            var authenticationHealthCheckResponse = await _healthCheckService.GetHealthCheckAsync(_authenticationHealthCheckUrl);

            if (authenticationHealthCheckResponse.Status is not HealthCheckStatus.Healthy)
            {
                return Redirect($"~/{RouteFor.CannotReachAuthenticationProvider}");
            }
        }

        if (!string.IsNullOrWhiteSpace(_authorizationHealthCheckUrl))
        {
            var authorizationHealthCheckResponse = await _healthCheckService.GetHealthCheckAsync(_authorizationHealthCheckUrl);

            if (authorizationHealthCheckResponse.Status is not HealthCheckStatus.Healthy)
            {
                return Redirect($"~/{RouteFor.CannotReachAuthorizationProvider}");
            }
        }

        if (string.IsNullOrWhiteSpace(returnUrl))
        {
            returnUrl = Url.Content("~/");
        }

        if (HttpContext.User.Identity is not null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Response.Redirect(returnUrl);
            }
        }

        return Challenge(new AuthenticationProperties { RedirectUri = returnUrl }, OpenIdConnectDefaults.AuthenticationScheme);
    }
}
