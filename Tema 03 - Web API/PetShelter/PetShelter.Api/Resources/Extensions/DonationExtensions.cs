namespace PetShelter.Api.Resources.Extensions
{
    public static class DonationExtensions
    {
        public static Domain.Donation AsDomainModel(this CreatedDonation donation)
        {
            var domainModel = new Domain.Donation(donation.Amount);
            domainModel.Name = donation.Name;
            domainModel.Donor = donation.Donor.AsDomainModel();
            return domainModel;
        }
    }
}
