using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBank.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.FullName)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(u => u.CellPhoneNumber, cellPhone =>
        {
            cellPhone.Property(c => c.Value)
                .HasColumnName("CellPhoneNumber")
                .IsRequired()
                .HasMaxLength(15);

            cellPhone.HasIndex(c => c.Value).IsUnique();
        });

        builder.OwnsOne(u => u.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(250);

            email.HasIndex(e => e.Value).IsUnique();
        });

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired();

        builder.Property(u => u.IsActive)
            .IsRequired();
    }
}
