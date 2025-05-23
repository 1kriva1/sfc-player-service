using AutoMapper;

using SFC.Player.Application.Interfaces.Reference;
using SFC.Player.Domain.Common;

namespace SFC.Player.Infrastructure.Services.Reference;
public abstract class BaseReference<TEntity, TId, TDto>(IMapper mapper)
    : IReference<TEntity, TId, TDto> where TEntity : BaseEntity<TId>
{
    private readonly IMapper _mapper = mapper;

    protected abstract Task<TEntity?> GetFromLocalAsync(TId id, CancellationToken cancellationToken = default);

    protected abstract Task<TDto?> GetFromReferenceAsync(TId id, CancellationToken cancellationToken = default);

    protected abstract Task<TEntity> AddLocalAsync(TEntity entity, CancellationToken cancellationToken = default);

    public async Task<TEntity?> GetAsync(TId id, CancellationToken cancellationToken = default)
    {
        TEntity? entity = await GetFromLocalAsync(id, cancellationToken).ConfigureAwait(true);

        if (entity is not null) return entity;

        TDto? reference = await GetFromReferenceAsync(id, cancellationToken).ConfigureAwait(true);

        if (reference is not null)
        {
            entity = _mapper.Map<TEntity>(reference);

            await AddLocalAsync(entity, cancellationToken).ConfigureAwait(false);
        }

        return entity;
    }
}
