namespace PetShelter.Api.Resources
{
    public class IdentifiableDonation : Donation
    {
        public int Id { get; set; }

        public Person Donor { get; set; }

        public Fund Fund { get; set; }
    }
}
