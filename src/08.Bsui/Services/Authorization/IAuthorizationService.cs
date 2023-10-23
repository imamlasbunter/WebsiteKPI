using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetAuthorizationInfo;
using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetPositions;

namespace Pertamina.Website_KPI.Bsui.Services.Authorization;
public interface IAuthorizationService
{
    Task<GetPositionsResponse> GetPositionsAsync(string username, string accessToken, CancellationToken cancellationToken = default);
    Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, string accessToken, CancellationToken cancellationToken = default);
}
