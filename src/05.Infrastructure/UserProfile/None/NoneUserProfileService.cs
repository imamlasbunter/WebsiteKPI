﻿using Microsoft.Extensions.Logging;
using Pertamina.Website_KPI.Application.Services.UserProfile;
using Pertamina.Website_KPI.Application.Services.UserProfile.Models.GetUserProfile;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Extensions;

namespace Pertamina.Website_KPI.Infrastructure.UserProfile.None;
public class NoneUserProfileService : IUserProfileService
{
    private readonly ILogger<NoneUserProfileService> _logger;

    public NoneUserProfileService(ILogger<NoneUserProfileService> logger)
    {
        _logger = logger;
    }

    private void LogWarning()
    {
        _logger.LogWarning("{ServiceName} is set to None.", $"{nameof(UserProfile).SplitWords()} {CommonDisplayTextFor.Service}");
    }

    public Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken)
    {
        LogWarning();

        return Task.FromResult(new GetUserProfileResponse());
    }
}
