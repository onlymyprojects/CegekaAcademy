namespace PetShelter.Domain
{
    public class Donation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int DonorId { get; set; }
        public int PetId { get; set; }

        public Person Donor { get; set; }
        public Fund Fund { get; set; }

        public Donation(decimal amount, int id = 0)
        {
            Amount = amount;
            Id = id;
        }
    }
}
