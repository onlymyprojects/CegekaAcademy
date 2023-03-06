using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DataAccess;
using PetShelter.Domain.Extensions.DomainModel;
using System.Collections.Immutable;

namespace PetShelter.Domain.Services
{
    public class FundService : IFundService
    {
        private readonly IFundRepository _fundRepository;
        private readonly IPersonRepository _personRepository;

        public FundService(IFundRepository fundRepository, IPersonRepository personRepository)
        {
            _fundRepository = fundRepository;
            _personRepository = personRepository;
        }

        public async Task<IReadOnlyCollection<Fund>> GetAllFunds()
        {
            var funds = await _fundRepository.GetAll();
            return funds.Select(p => p.ToDomainModel())
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

        public async Task DeleteFundAsync(int fundId)
        {
            var deletedFund = await _fundRepository.GetById(fundId);
            deletedFund.Status = "Closed";
            await _fundRepository.Update(deletedFund);
        }
    }
}
