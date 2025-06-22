using BloodBank.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BloodBank.Infrastructure.Persistence.Configurations;

public class BloodTransferConfiguration : IEntityTypeConfiguration<BloodTransfer>
{
    public void Configure(EntityTypeBuilder<BloodTransfer> builder)
    {
        builder.ToTable("BloodTransfers");

        builder.HasKey(bt => bt.Id);

        builder.Property(bt => bt.TransferDate)
            .IsRequired();

        builder.Property(bt => bt.BloodType)
            .IsRequired();

        builder.Property(bt => bt.RhFactor)
            .IsRequired();

        builder.Property(bt => bt.QuantityML)
           .IsRequired();

        builder.HasOne(bt => bt.Hospital)
            .WithMany(h => h.BloodTransfers)
            .HasForeignKey(bt => bt.HospitalId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
