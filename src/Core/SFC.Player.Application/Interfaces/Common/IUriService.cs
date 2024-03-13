namespace SFC.Player.Application.Interfaces.Common;
public interface IUriService
{
    public Uri GetPageUri(string queryString, string route, int page);
}
