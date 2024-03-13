using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using PlayerUser = SFC.Player.Domain.Entities.User;

namespace SFC.Player.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<PlayerUser>
{
    public void Configure(EntityTypeBuilder<PlayerUser> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
