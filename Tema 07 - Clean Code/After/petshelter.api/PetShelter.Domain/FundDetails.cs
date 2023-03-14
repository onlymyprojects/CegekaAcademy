namespace PetShelter.Domain
{
    public class FundDetails
    {
        public string Name { get; set; }
        public decimal TotalDonationAmount { get; set; }
        public decimal Goal { get; set; }
        public DateTime DueDate { get; set; }
    }
}
