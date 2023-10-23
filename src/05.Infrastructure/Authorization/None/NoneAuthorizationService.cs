using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Application.Services.Authorization;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetAuthorizationInfo;

namespace Pertamina.Website_KPI.Infrastructure.Authorization.None;
public class NoneAuthorizationService : IAuthorizationService
{
    private readonly ILogger<NoneAuthorizationService> _logger;

    public NoneAuthorizationService(ILogger<NoneAuthorizationService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(Authorization)} {CommonDisplayTextFor.Service}");
    }

    public Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetAuthorizationInfoResponse());
    }
}
