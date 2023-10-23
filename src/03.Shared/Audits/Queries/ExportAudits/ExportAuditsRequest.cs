using Pertamina.Website_KPI.Shared.Common.Attributes;
using Pertamina.Website_KPI.Shared.Common.Constants;

namespace Pertamina.Website_KPI.Shared.Audits.Queries.ExportAudits;
public class ExportAuditsRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public IList<Guid> AuditIds { get; set; } = new List<Guid>();
}
