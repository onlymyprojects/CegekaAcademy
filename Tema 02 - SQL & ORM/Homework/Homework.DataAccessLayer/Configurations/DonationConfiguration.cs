using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Homework.Common.Enums;

namespace Homework.DataAccessLayer.Configurations
{
    public class DonationConfiguration : IEntityTypeConfiguration<Donation>
    {
        public void Configure(EntityTypeBuilder<Donation> builder)
        {
            //Primary key
            builder.HasKey(p => p.Id);

            //Columns mapping and constraints
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Currency)
                .HasConversion(p => p.ToString(), p => (Currencies)Enum.Parse(typeof(Currencies), p))
                .IsRequired().HasMaxLength(3);
            builder.Property(p => p.Amount).IsRequired();
            builder.Property(p => p.Destination)
                .HasConversion(p => p.ToString(), p => (DonationDestination)Enum.Parse(typeof(DonationDestination), p))
                .IsRequired().HasMaxLength(100);
            builder.Property(p => p.DonorId).IsRequired();

            //Relationships
            builder.HasOne(p => p.Donor).WithMany(p => p.Donations).HasForeignKey(p => p.DonorId)
                .IsRequired();
        }
    }
}
