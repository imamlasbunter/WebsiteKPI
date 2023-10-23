using Pertamina.Website_KPI.Shared.Services.Authorization.Models.GetAuthorizationInfo;

namespace Pertamina.Website_KPI.Application.Services.Authorization;
public interface IAuthorizationService
{
    Task<GetAuthorizationInfoResponse> GetAuthorizationInfoAsync(string positionId, CancellationToken cancellationToken);
}
