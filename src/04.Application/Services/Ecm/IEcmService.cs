using Pertamina.Website_KPI.Application.Services.Ecm.Models.UploadContent;

namespace Pertamina.Website_KPI.Application.Services.Ecm;
public interface IEcmService
{
    Task<UploadContentResponse> UploadContentAsync(UploadContentRequest request, CancellationToken cancellationToken);
}
