namespace PetShelter.Domain
{
    public class Donation : DonationInfo, INamedEntity
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }

        public Person Donor { get; set; }

        public Donation(decimal amount, int id = 0)
        {
            Amount = amount;
            Id = id;
        }
    }
}
