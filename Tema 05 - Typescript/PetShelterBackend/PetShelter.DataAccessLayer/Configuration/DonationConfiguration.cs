using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Configuration;

public class DonationConfiguration : IEntityTypeConfiguration<Donation>
{
    public void Configure(EntityTypeBuilder<Donation> builder)
    {
        //Primary key
        builder.HasKey(p => p.Id);

        //Columns mapping and constraints
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Amount).IsRequired();
        builder.Property(p => p.DonorId).IsRequired();
        builder.Property(p => p.FundId).IsRequired();

        //Relationships
        builder.HasOne(p => p.Donor).WithMany(p => p.Donations).HasForeignKey(p => p.DonorId)
            .IsRequired();
        builder.HasOne(p => p.Fund).WithMany(p => p.FundDonations).HasForeignKey(p => p.FundId)
            .IsRequired();
    }
}