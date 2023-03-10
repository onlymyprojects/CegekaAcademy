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

        public static IdentifiableDonation AsResource(this Domain.Donation donation)
        {
            return new IdentifiableDonation
            {
                Id = donation.Id,
                Name = donation.Name,
                Amount = donation.Amount,
                Donor = donation.Donor?.AsResource(),
                Fund = donation.Fund?.AsResource()
            };
        }
    }
}
