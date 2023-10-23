using Pertamina.Website_KPI.Application.Services.Email.Models.SendEmail;

namespace Pertamina.Website_KPI.Application.Services.Email;
public interface IEmailService
{
    Task SendEmailAsync(SendEmailRequest sendEmailRequest);
}
