using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Homework.Common.Enums;

namespace Homework.DataAccessLayer.Configurations
{
    public class PetConfiguration : IEntityTypeConfiguration<Pet>
    {
        public void Configure(EntityTypeBuilder<Pet> builder)
        {
            //Primary key
            builder.HasKey(p => p.Id);

            //Columns mapping and constraints
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.Description).HasMaxLength(5000);
            builder.Property(p => p.Type).HasConversion(p => p.ToString(), p => (PetType)Enum.Parse(typeof(PetType), p))
                .IsRequired().HasMaxLength(100);
            builder.Property(p => p.WeightInKg).IsRequired();
            builder.Property(p => p.IsHealthy).HasConversion(v => v ? "Yes" : "No", v => v == "Yes").IsRequired().HasMaxLength(3);
            builder.Property(p => p.IsSheltered).HasConversion(v => v ? "Yes" : "No", v => v == "Yes").IsRequired().HasMaxLength(3);

            //Relationships
            builder.HasOne(p => p.Rescuer).WithMany(p => p.RescuedPets).HasForeignKey(p => p.RescuerId)
                .IsRequired(false);

            builder.HasOne(p => p.Adopter).WithMany(p => p.AdoptedPets).HasForeignKey(p => p.AdopterId)
                .IsRequired(false);
        }
    }
}
