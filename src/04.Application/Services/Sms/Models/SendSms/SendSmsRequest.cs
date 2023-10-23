namespace Pertamina.Website_KPI.Application.Services.Sms.Models.SendSms;

public class SendSmsRequest
{
    public string Id { get; set; } = default!;
    public string To { get; set; } = default!;
    public string Message { get; set; } = default!;
}
