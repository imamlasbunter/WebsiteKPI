﻿using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetAuthorizationInfo;
using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetPositions;

namespace Pertamina.Website_KPI.Bsui.Services.Authorization.None;
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

    public Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, string accessToken, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetAuthorizationInfoResponse());
    }

    public Task<GetPositionsResponse> GetPositionsAsync(string username, string accessToken, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetPositionsResponse());
    }
}
