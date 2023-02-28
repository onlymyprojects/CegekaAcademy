using Homework.DataAccessLayer;
using Homework.DataAccessLayer.Models;
using Homework.DataAccessLayer.Repositories;

namespace HomeWork.Domain.Services
{
    public class FundRaiser
    {
        private readonly IFundRepository fundRepository;
        private readonly IPersonRepository personRepository;
        private readonly IDonationRepository donationRepository;

        public FundRaiser()
        {
            fundRepository = new FundRepository(new HomeworkContext());
            personRepository = new PersonRepository(new HomeworkContext());
            donationRepository = new DonationRepository(new HomeworkContext());
        }

        public async Task AddFunds(Fund fund)
        {
            await fundRepository.Add(fund);
        }

        public async Task<Fund> PetById(int id)
        {
            return await fundRepository.GetFundById(id);
        }

        public async Task<Person?> PersonByIdNumber(string IdNumber)
        {
            return await personRepository.GetPersonByIdNumber(IdNumber);
        }

        public async Task<List<Fund>> GetAllFunds()
        {
            return await fundRepository.GetAll();
        }

        public async Task Donate(Donation fundRaiserDonadion)
        {
            await donationRepository.Add(fundRaiserDonadion);
        }

        public async Task<Person?> PersonById(int? id)
        {
            return await personRepository.GetPersonById(id);
        }
    }
}
