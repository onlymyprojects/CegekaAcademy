namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class FundExtensions
    {
        public static Fund? ToDomainModel(this DataAccessLayer.Models.Fund fund)
        {
            if (fund == null)
            {
                return null;
            }

            var fundStatus = Enum.Parse<FundStatus>(fund.Status);
            var domainModel = new Fund(fundStatus, id: fund.Id);
            domainModel.Name = fund.Name;
            domainModel.DonationAmout = fund.DonationAmout;
            domainModel.Goal = fund.Goal;
            domainModel.DueDate = fund.DueDate;
            domainModel.Owner = fund.Owner.ToDomainModel();
            return domainModel;
        }
    }
}
