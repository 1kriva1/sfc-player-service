using SFC.Players.Application.Interfaces.Common;

namespace SFC.Players.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;

    public DateTime DateNow => DateTime.UtcNow.Date;
}
