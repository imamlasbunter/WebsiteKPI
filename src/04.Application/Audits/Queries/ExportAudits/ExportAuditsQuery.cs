using System.Globalization;
using CsvHelper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Application.Common.Attributes;
using Pertamina.Website_KPI.Application.Services.DateAndTime;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Shared.Audits.Queries.ExportAudits;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Services.Authorization.Constants;

namespace Pertamina.Website_KPI.Application.Audits.Queries.ExportAudits;
[Authorize(Policy = Permissions.SolTem_Audit_Index)]
public class ExportAuditsQuery : ExportAuditsRequest, IRequest<ExportAuditsResponse>
{
}

public class ExportAuditsQueryHandler : IRequestHandler<ExportAuditsQuery, ExportAuditsResponse>
{
    private readonly IWebsite_KPIDbContext _context;
    private readonly IDateAndTimeService _dateTime;

    public ExportAuditsQueryHandler(IWebsite_KPIDbContext context, IDateAndTimeService dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<ExportAuditsResponse> Handle(ExportAuditsQuery request, CancellationToken cancellationToken)
    {
        var audits = await _context.Audits
                .Where(x => request.AuditIds.Contains(x.Id))
                .ToListAsync(cancellationToken);

        using var memoryStream = new MemoryStream();
        using var streamWriter = new StreamWriter(memoryStream);
        using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
        csvWriter.WriteRecords(audits);

        var content = memoryStream.ToArray();

        return new ExportAuditsResponse
        {
            ContentType = ContentTypes.TextCsv,
            Content = content,
            FileName = $"Audits_{audits.Count}_{_dateTime.Now:yyyyMMdd_HHmmss}.csv"
        };
    }
}
