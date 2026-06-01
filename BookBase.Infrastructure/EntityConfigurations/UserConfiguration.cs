using BookBase.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBase.Infrastructure.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder
            .HasIndex(u => u.Username)
            .IsUnique();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();
    }
}