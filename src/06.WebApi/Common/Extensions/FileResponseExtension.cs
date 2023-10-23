using Microsoft.AspNetCore.Mvc;
using Pertamina.Website_KPI.Shared.Common.Responses;

namespace Pertamina.Website_KPI.WebApi.Common.Extensions;
public static class FileResponseExtension
{
    public static FileContentResult AsFile(this FileResponse fileResponse)
    {
        return new FileContentResult(fileResponse.Content, fileResponse.ContentType)
        {
            FileDownloadName = fileResponse.FileName
        };
    }
}
