using Homework.DataAccessLayer.Configurations;
using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.DataAccessLayer
{
    public class HomeworkContext : DbContext
    {
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=Homework;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PetConfiguration());
            modelBuilder.ApplyConfiguration(new FundConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new DonationConfiguration());
        }
    }
}
