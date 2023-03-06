using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Services
{
    public interface IDonationService
    {
        Task<IReadOnlyCollection<Donation>> GetAllDonations();
        Task<int> DonateAsync(Person donor, Donation donation);
    }
}
