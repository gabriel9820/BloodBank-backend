using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBank.Infrastructure.Persistence.Configurations;

public class HospitalConfiguration : IEntityTypeConfiguration<Hospital>
{
    public void Configure(EntityTypeBuilder<Hospital> builder)
    {
        builder.ToTable("Hospitals");

        builder.HasKey(h => h.Id);

        builder.Property(h => h.Name)
            .IsRequired()
            .HasMaxLength(250);

        builder.OwnsOne(h => h.LandlineNumber, landline =>
        {
            landline.Property(l => l.Value)
                .HasColumnName("LandlineNumber")
                .IsRequired()
                .HasMaxLength(15);

            landline.HasIndex(l => l.Value).IsUnique();
        });

        builder.OwnsOne(h => h.Address, address =>
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

        builder.HasMany(h => h.BloodTransfers)
            .WithOne(bt => bt.Hospital)
            .HasForeignKey(bt => bt.HospitalId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
