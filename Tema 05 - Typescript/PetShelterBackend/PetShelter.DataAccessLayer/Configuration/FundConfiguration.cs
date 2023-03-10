using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;
namespace PetShelter.DataAccessLayer.Configuration
{
    public class FundConfiguration : IEntityTypeConfiguration<Fund>
    {
        public void Configure(EntityTypeBuilder<Fund> builder)
        {
            //Primary key
            builder.HasKey(p => p.Id);

            //Columns mapping and constraints
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.DonationAmout).IsRequired();
            builder.Property(p => p.Goal).IsRequired();
            builder.Property(p => p.CreationDate).IsRequired();
            builder.Property(p => p.DueDate).IsRequired();
            builder.Property(p => p.Status).IsRequired().HasMaxLength(250);

            //Relationships
            builder.HasOne(p => p.Owner).WithMany(p => p.Funds).HasForeignKey(p => p.OwnerId)
                .IsRequired(false);
        }
    }
}
