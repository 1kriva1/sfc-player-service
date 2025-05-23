using SFC.Player.Application.Interfaces.Persistence.Repository.Data;
using SFC.Player.Domain.Common;
using SFC.Player.Infrastructure.Persistence.Contexts;
using SFC.Player.Infrastructure.Persistence.Repositories.Common.Data;

namespace SFC.Player.Infrastructure.Persistence.Repositories.Data;
public class DataRepository<T, TEnum>(DataDbContext context)
    : DataRepository<T, DataDbContext, TEnum>(context), IDataRepository<T, TEnum>
     where T : EnumDataEntity<TEnum>
     where TEnum : struct
{ }
