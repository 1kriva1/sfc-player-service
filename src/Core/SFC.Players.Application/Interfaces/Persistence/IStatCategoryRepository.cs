namespace SFC.Players.Application.Interfaces.Persistence;
public interface IStatCategoryRepository
{
    Task<int> CountAsync(IEnumerable<int> categories);
}
