namespace Pertamina.Website_KPI.Bsui.Services.Telemetry.ApplicationInsights;

public class ApplicationInsightsTelemetryOptions
{
    public static readonly string SectionKey = $"{nameof(Telemetry)}:{nameof(ApplicationInsights)}";

    public string ConnectionString { get; set; } = default!;
}
