using Pertamina.Website_KPI.Application.Services.DateAndTime;

namespace Pertamina.Website_KPI.Infrastructure.DateAndTime;
public class DateAndTimeService : IDateAndTimeService
{
    public DateTimeOffset Now => DateTimeOffset.Now;
}
