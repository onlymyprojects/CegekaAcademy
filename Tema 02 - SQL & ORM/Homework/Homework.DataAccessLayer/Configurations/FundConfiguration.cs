using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Homework.DataAccessLayer.Configurations
{
    public class FundConfiguration : IEntityTypeConfiguration<Fund>
    {
        public void Configure(EntityTypeBuilder<Fund> builder)
        {
            //Primary key
            builder.HasKey(p => p.Id);

            //Columns mapping and constraints
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(5000);
            builder.Property(p => p.Target).HasMaxLength(1000);

            //Relationships
            builder.HasOne(p => p.Owner).WithMany(p => p.Funds).HasForeignKey(p => p.OwnerId)
                .IsRequired(false);
        }
    }
}
