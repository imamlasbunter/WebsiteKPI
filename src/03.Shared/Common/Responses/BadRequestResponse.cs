﻿namespace Pertamina.Website_KPI.Shared.Common.Responses;

public class BadRequestResponse : ErrorResponse
{
    public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();

    public override IList<string> Details => Errors.SelectMany(x => x.Value).ToList();
}
