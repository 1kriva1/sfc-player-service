using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

using SFC.Player.Application.Common.Constants;
using SFC.Player.Domain.Entities.Identity;

using PlayerUser = SFC.Player.Domain.Entities.User;

namespace SFC.Player.Infrastructure.Persistence.Configurations.Identity;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    private readonly bool _isSqlServer;

    public UserConfiguration(bool isSqlServer)
    {
        _isSqlServer = isSqlServer;
    }

    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.PlayerUser)
               .WithOne(e => e.IdentityUser)
               .HasForeignKey<PlayerUser>(u => u.Id)
               .IsRequired(true);

        if (_isSqlServer)
        {
            builder.ToTable("Users", DatabaseConstants.IDENTITY_SCHEMA_NAME);
        }
        else
        {
            builder.ToTable("Identity_Users");
        }        
    }
}
