﻿using SFC.Player.Application.Interfaces.Common;

namespace SFC.Player.Infrastructure.Services.Common;

public class DateTimeService : IDateTimeService
{
    public DateTime Now => DateTime.UtcNow;

    public DateTime DateNow => DateTime.UtcNow.Date;
}
