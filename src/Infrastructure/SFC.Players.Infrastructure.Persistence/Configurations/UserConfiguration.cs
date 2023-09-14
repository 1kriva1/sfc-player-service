using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using SFC.Players.Domain.Entities;

namespace SFC.Players.Infrastructure.Persistence.Configurations;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.UserId);

        builder.HasOne(e => e.IdentityUser)
               .WithOne(e => e.User)
               .HasForeignKey<IdentityUser>(u => u.UserId)
               .IsRequired(false);
    }
}
