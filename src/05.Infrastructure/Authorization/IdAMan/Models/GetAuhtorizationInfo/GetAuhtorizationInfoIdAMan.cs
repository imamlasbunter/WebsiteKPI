﻿using Newtonsoft.Json;

namespace Pertamina.Website_KPI.Infrastructure.Authorization.IdAMan.Models.GetAuhtorizationInfo;
public class GetAuhtorizationInfoIdAMan
{
    [JsonProperty("application")]
    public GetAuhtorizationInfoIdAManApplication Application { get; set; } = default!;

    [JsonProperty("roles")]
    public GetAuhtorizationInfoIdAManRole[] Roles { get; set; } = Array.Empty<GetAuhtorizationInfoIdAManRole>();

    [JsonProperty("customParameters")]
    public IEnumerable<IDictionary<string, string>> CustomParameters { get; set; } = default!;
}
