using Homework.Common.Enums;

namespace Homework.DataAccessLayer.Models
{
    public class Donation : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Currencies Currency { get; set; }
        public decimal Amount { get; set; }
        public DonationDestination Destination { get; set; }
        public int DonorId { get; set; }

        public Person Donor { get; set; }
    }
}
