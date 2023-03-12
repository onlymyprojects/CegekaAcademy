namespace PetShelter.Api.Resources
{
    public class CreatedDonation : Donation
    {
        public Person Donor { get; set; }
    }
}
