namespace SFC.Player.Application.Features.Common.Dto.Pagination;

public class PageDto<T>
{
    public IEnumerable<T> Items { get; set; } = default!;

    public PageMetadataDto Metadata { get; set; } = default!;
}
