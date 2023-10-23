﻿using Newtonsoft.Json;

namespace Pertamina.Website_KPI.Bsui.Services.Authorization.IdAMan.Models.GetPositions;
public class GetPositionsIdAManPersona
{
    [JsonProperty("type")]
    public string Type { get; set; } = default!;

    [JsonProperty("positions")]
    public GetPositionsIdAManPosition[] Positions { get; set; } = default!;
}
