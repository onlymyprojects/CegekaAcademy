using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DataAccess;
using PetShelter.Domain.Extensions.DomainModel;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.Domain.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;
        private readonly IPersonRepository _personRepository;

        public DonationService(IDonationRepository donationRepository, IPersonRepository personRepository)
        {
            _donationRepository = donationRepository;
            _personRepository = personRepository;
        }

        public async Task<IReadOnlyCollection<Donation>> GetAllDonations()
        {
            var funds = await _donationRepository.GetAll();
            return funds.Select(p => p.ToDomainModel())
                .ToImmutableArray();
        }

        public async Task<int> DonateAsync(Person donor, Donation donation)
        {
            var person = await _personRepository.GetOrAddPersonAsync(donor.FromDomainModel());
            var createdDonation = new DataAccessLayer.Models.Donation
            {
                Name = donation.Name,
                Amount = donation.Amount,
                Donor = person,
                DonorId = person.Id,
            };

            await _donationRepository.Add(createdDonation);
            return createdDonation.Id;
        }
    }
}
