using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Homework.DataAccessLayer.Configurations
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            //Primary key
            builder.HasKey(p => p.Id);

            //Columns mapping and constraints
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
            builder.Property(p => p.DateOfBirth).HasColumnType("date").IsRequired();
            builder.Property(p => p.IdNumber).HasMaxLength(50).IsRequired();
        }
    }
}
