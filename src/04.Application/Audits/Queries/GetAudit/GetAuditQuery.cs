using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Pertamina.Website_KPI.Application.Common.Attributes;
using Pertamina.Website_KPI.Application.Common.Exceptions;
using Pertamina.Website_KPI.Application.Common.Mappings;
using Pertamina.Website_KPI.Application.Services.Persistence;
using Pertamina.Website_KPI.Domain.Entities;
using Pertamina.Website_KPI.Shared.Audits.Constants;
using Pertamina.Website_KPI.Shared.Audits.Queries.GetAudit;
using Pertamina.Website_KPI.Shared.Services.Authorization.Constants;

namespace Pertamina.Website_KPI.Application.Audits.Queries.GetAudit;
[Authorize(Policy = Permissions.SolTem_Audit_View)]
public class GetAuditQuery : IRequest<GetAuditResponse>
{
    public Guid AuditId { get; set; }
}

public class GetAuditResponseMapping : IMapFrom<Audit, GetAuditResponse>
{
}

public class GetAuditQueryHandler : IRequestHandler<GetAuditQuery, GetAuditResponse>
{
    private readonly IWebsite_KPIDbContext _context;
    private readonly IMapper _mapper;

    public GetAuditQueryHandler(IWebsite_KPIDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetAuditResponse> Handle(GetAuditQuery request, CancellationToken cancellationToken)
    {
        var audit = await _context.Audits
            .AsNoTracking()
            .Where(x => x.Id == request.AuditId)
            .SingleOrDefaultAsync(cancellationToken);

        if (audit is null)
        {
            throw new NotFoundException(DisplayTextFor.Audit, request.AuditId);
        }

        return _mapper.Map<GetAuditResponse>(audit);
    }
}
