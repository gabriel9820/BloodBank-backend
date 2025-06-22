using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBank.Infrastructure.Persistence.Configurations;

public class DonorConfiguration : IEntityTypeConfiguration<Donor>
{
    public void Configure(EntityTypeBuilder<Donor> builder)
    {
        builder.ToTable("Donors");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.FullName)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(d => d.CellPhoneNumber, cellPhone =>
        {
            cellPhone.Property(c => c.Value)
                .HasColumnName("CellPhoneNumber")
                .IsRequired()
                .HasMaxLength(15);

            cellPhone.HasIndex(c => c.Value).IsUnique();
        });

        builder.OwnsOne(d => d.Email, email =>
        {
            email.Property(e => e.Value)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(250);

            email.HasIndex(e => e.Value).IsUnique();
        });

        builder.Property(d => d.BirthDate)
            .IsRequired();

        builder.Property(d => d.Gender)
            .IsRequired();

        builder.Property(d => d.Weight)
            .IsRequired()
            .HasPrecision(5, 2);

        builder.Property(d => d.BloodType)
            .IsRequired();

        builder.Property(d => d.RhFactor)
           .IsRequired();

        builder.OwnsOne(d => d.Address, address =>
        {
            address.Property(a => a.Street)
                .HasColumnName("Street")
                .IsRequired()
                .HasMaxLength(100);

            address.Property(a => a.Number)
                .HasColumnName("Number")
                .IsRequired()
                .HasMaxLength(10);

            address.Property(a => a.Neighborhood)
                .HasColumnName("Neighborhood")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.City)
                .HasColumnName("City")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.State)
                .HasColumnName("State")
                .IsRequired()
                .HasMaxLength(50);

            address.Property(a => a.ZipCode)
                .HasColumnName("ZipCode")
                .IsRequired()
                .HasMaxLength(9);
        });

        builder.HasMany(d => d.Donations)
            .WithOne(dn => dn.Donor)
            .HasForeignKey(dn => dn.DonorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
