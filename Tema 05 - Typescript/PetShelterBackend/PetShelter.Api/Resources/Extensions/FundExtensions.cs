using PetShelter.Domain;

namespace PetShelter.Api.Resources.Extensions
{
    public static class FundExtensions
    {
        public static Domain.FundInfo AsFundInfo(this Fund fund)
        {
            return new Domain.FundInfo
            {
                Name = fund.Name,
                DonationAmout = fund.DonationAmout,
                Goal = fund.Goal,
                DueDate = fund.DueDate
            };
        }

        public static Domain.Fund AsDomainModel(this CreatedFund fund)
        {
            var fundStatus = Enum.Parse<FundStatus>(fund.Status);
            var domainModel = new Domain.Fund(fundStatus);
            domainModel.Name = fund.Name;
            domainModel.DonationAmout = fund.DonationAmout;
            domainModel.Goal = fund.Goal;
            domainModel.DueDate = fund.DueDate;
            domainModel.Owner = fund.Owner.AsDomainModel();
            return domainModel;
        }

        public static IdentifiableFund AsResource(this Domain.Fund fund)
        {
            return new IdentifiableFund
            {
                Id = fund.Id,
                Name = fund.Name,
                DonationAmout = fund.DonationAmout,
                Goal = fund.Goal,
                Status = fund.Status.ToString(),
                DueDate = fund.DueDate,
                Owner = fund.Owner?.AsResource()
            };
        }
    }
}
