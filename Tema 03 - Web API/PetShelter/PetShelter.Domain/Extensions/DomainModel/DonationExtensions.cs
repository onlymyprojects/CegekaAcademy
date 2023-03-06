using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Extensions.DomainModel
{
    internal static class DonationExtensions
    {
        public static Donation? ToDomainModel(this DataAccessLayer.Models.Donation donation)
        {
            if (donation == null)
            {
                return null;
            }

            var domainModel = new Donation(donation.Amount, id: donation.Id);
            domainModel.Name = donation.Name;
            domainModel.Amount = donation.Amount;
            domainModel.Donor = donation.Donor.ToDomainModel();
            return domainModel;
        }
    }
}
