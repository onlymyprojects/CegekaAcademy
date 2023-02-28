using Homework.DataAccessLayer;
using Homework.DataAccessLayer.Models;
using Homework.DataAccessLayer.Repositories;

namespace HomeWork.Domain.Services
{
    public class PetShelter
    {
        private readonly IPetRepository petRepository;
        private readonly IPersonRepository personRepository;
        private readonly IDonationRepository donationRepository;

        public PetShelter()
        {
            petRepository = new PetRepository(new HomeworkContext());
            personRepository = new PersonRepository(new HomeworkContext());
            donationRepository = new DonationRepository(new HomeworkContext());
        }

        public async Task AddPets(Pet pet)
        {
            await petRepository.Add(pet);
        }

        public async Task<Person?> PersonByIdNumber(string IdNumber)
        {
            return await personRepository.GetPersonByIdNumber(IdNumber);
        }

        public async Task<Person?> PersonById(int? id)
        {
            return await personRepository.GetPersonById(id);
        }

        public async Task<List<Pet>> GetAllPets()
        {
            return await petRepository.GetAll();
        }

        public async Task<Pet> PetById(int id)
        {
            return await petRepository.GetPetById(id);
        }

        public async Task ModifyExistingPet(Pet pet)
        {
            await petRepository.ModifyPet(pet);
        }

        public async Task Donate(Donation petShelterDonadion)
        {
            await donationRepository.Add(petShelterDonadion);
        }

        public async Task<List<Donation>> GetAllDonations()
        {
            return await donationRepository.GetAll();
        }
    }
}
