using Pertamina.Website_KPI.Application.Services.UserProfile.Models.GetUserProfile;

namespace Pertamina.Website_KPI.Application.Services.UserProfile;
public interface IUserProfileService
{
    Task<GetUserProfileResponse> GetUserProfileAsync(string username, CancellationToken cancellationToken);
}
