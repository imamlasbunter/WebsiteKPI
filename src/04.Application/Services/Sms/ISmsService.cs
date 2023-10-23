using Pertamina.Website_KPI.Application.Services.Sms.Models.SendSms;

namespace Pertamina.Website_KPI.Application.Services.Sms;
public interface ISmsService
{
    Task SendSmsAsync(SendSmsRequest sendSmsRequest);
}
