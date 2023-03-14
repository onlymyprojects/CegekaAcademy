namespace PetShelter.DataAccessLayer.Models
{
    public class Fund : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal TotalDonationAmount { get; set; }
        public decimal Goal { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public string Status { get; set; }
        public int? OwnerId { get; set; }

        public Person Owner { get; set; }

        public ICollection<Donation> FundDonations { get; set; }
    }
}
