using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Exceptions;
using PetShelter.Domain.Extensions.DataAccess;
using PetShelter.Domain.Extensions.DomainModel;
using System.Collections.Immutable;

namespace PetShelter.Domain.Services
{
    public class FundService : IFundService
    {
        private readonly IFundRepository _fundRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IPersonRepository _personRepository;

        public FundService(IFundRepository fundRepository, IDonationRepository donationRepository, IPersonRepository personRepository)
        {
            _fundRepository = fundRepository;
            _donationRepository = donationRepository;
            _personRepository = personRepository;
        }

        public async Task<IReadOnlyCollection<Fund>> GetAllFunds()
        {
            var funds = await _fundRepository.GetAll();
            return funds.Select(p => p.ToDomainModel())
                .ToImmutableArray();
        }

        public async Task<IReadOnlyCollection<Donation>> GetAllDonations()
        {
            var donations = await _donationRepository.GetAll();
            return donations.Select(p => p.ToDomainModel())
                .ToImmutableArray();
        }

        public async Task<int> CreateFundAsync(Person owner, Fund fund)
        {
            var person = await _personRepository.GetOrAddPersonAsync(owner.FromDomainModel());
            var createdFund = new DataAccessLayer.Models.Fund
            {
                Name = fund.Name,
                Goal = fund.Goal,
                DueDate = fund.DueDate,
                Status = fund.Status.ToString(),
                Owner = person,
                OwnerId = person.Id,
            };

            if (createdFund.DueDate <= DateTime.UtcNow)
            {
                createdFund.Status = FundStatus.Closed.ToString();
            }

            await _fundRepository.Add(createdFund);
            return createdFund.Id;
        }

        public async Task<Fund> GetFund(int fundId)
        {
            var fund = await _fundRepository.GetById(fundId);
            if (fund == null)
            {
                return null;
            }
            fund.Owner = await _personRepository.GetById(fund.OwnerId.Value);

            return fund.ToDomainModel();
        }

        public async Task<int> AddDonationAsync(int fundId, Donation donation)
        {
            var savedFund = await _fundRepository.GetFundById(fundId);

            if (savedFund == null)
            {
                throw new NotFoundException($"Fund with id {fundId} not found.");
            }

            var savedDonor = await _personRepository.GetOrAddPersonAsync(donation.Donor.FromDomainModel());

            var createdDonation = new DataAccessLayer.Models.Donation
            {
                Name = donation.Name,
                Amount = donation.Amount,
                Donor = savedDonor,
                DonorId = savedDonor.Id,
                Fund = savedFund,
                FundId = savedFund.Id
            };

            savedFund.DonationAmout += createdDonation.Amount;
            if (savedFund.DonationAmout >= savedFund.Goal)
            {
                savedFund.Status = FundStatus.Closed.ToString();
            }
            await _fundRepository.Update(savedFund);

            await _donationRepository.Add(createdDonation);

            return createdDonation.Id;
        }

        public async Task DeleteFundAsync(int fundId)
        {
            var deletedFund = await _fundRepository.GetById(fundId);
            deletedFund.Status = "Closed";
            await _fundRepository.Update(deletedFund);
        }
    }
}
