using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pertamina.Website_KPI.Application.Audits.Queries.ExportAudits;
using Pertamina.Website_KPI.Application.Audits.Queries.GetAudit;
using Pertamina.Website_KPI.Application.Audits.Queries.GetAudits;
using Pertamina.Website_KPI.Shared.Audits.Constants;
using Pertamina.Website_KPI.Shared.Audits.Queries.ExportAudits;
using Pertamina.Website_KPI.Shared.Audits.Queries.GetAudit;
using Pertamina.Website_KPI.Shared.Audits.Queries.GetAudits;
using Pertamina.Website_KPI.Shared.Common.Responses;
using Pertamina.Website_KPI.WebApi.Common.Extensions;

namespace Pertamina.Website_KPI.WebApi.Areas.V1.Controllers;
[Authorize]
[ApiVersion(ApiVersioning.V1.Number)]
public class AuditsController : ApiControllerBase
{
    [HttpGet]
    [Produces(typeof(PaginatedListResponse<GetAuditsAudit>))]
    public async Task<ActionResult<PaginatedListResponse<GetAuditsAudit>>> GetAudits([FromQuery] GetAuditsQuery query)
    {
        return await Mediator.Send(query);
    }

    [HttpGet(ApiEndpoint.V1.Audits.RouteTemplateFor.AuditId)]
    [Produces(typeof(GetAuditResponse))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GetAuditResponse>> GetAudit([FromRoute] Guid auditId)
    {
        return await Mediator.Send(new GetAuditQuery { AuditId = auditId });
    }

    [HttpGet(ApiEndpoint.V1.Audits.RouteTemplateFor.Export)]
    [Produces(typeof(ExportAuditsResponse))]
    public async Task<ActionResult> ExportAudits([FromQuery] IList<Guid> auditIds)
    {
        var query = new ExportAuditsQuery
        {
            AuditIds = auditIds
        };

        var response = await Mediator.Send(query);

        return response.AsFile();
    }
}
