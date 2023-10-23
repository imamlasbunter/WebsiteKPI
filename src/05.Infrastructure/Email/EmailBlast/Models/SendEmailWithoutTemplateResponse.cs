namespace Pertamina.Website_KPI.Infrastructure.Email.EmailBlast.Models;

public class SendEmailWithoutTemplateResponse
{
    public IList<Guid> EmailRequestIds { get; set; } = new List<Guid>();
}
