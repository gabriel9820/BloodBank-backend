using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBank.Infrastructure.Persistence.Configurations;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        builder.ToTable("Donations");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.DonationDate)
            .IsRequired();

        builder.Property(d => d.QuantityML)
            .IsRequired();

        builder.HasOne(d => d.Donor)
            .WithMany(dn => dn.Donations)
            .HasForeignKey(d => d.DonorId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
