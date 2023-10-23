﻿using FluentValidation;
using Pertamina.Website_KPI.Shared.Common.Attributes;
using Pertamina.Website_KPI.Shared.Common.Constants;
using Pertamina.Website_KPI.Shared.Common.Enums;

namespace Pertamina.Website_KPI.Shared.Common.Requests;
public class PaginatedListRequest
{
    [OpenApiContentType(ContentTypes.TextPlain)]
    public int Page { get; set; } = 1;

    [OpenApiContentType(ContentTypes.TextPlain)]
    public int PageSize { get; set; } = 10;

    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? SearchText { get; set; }

    [OpenApiContentType(ContentTypes.TextPlain)]
    public string? SortField { get; set; }

    [OpenApiContentType(ContentTypes.TextPlain)]
    public SortOrder? SortOrder { get; set; }
}

public class PaginatedListRequestValidator : AbstractValidator<PaginatedListRequest>
{
    public PaginatedListRequestValidator()
    {
        RuleFor(v => v.Page)
            .GreaterThan(0);

        RuleFor(v => v.PageSize)
            .GreaterThan(0)
            .LessThanOrEqualTo(100);

        RuleFor(v => v.SortOrder)
            .IsInEnum();
    }
}
