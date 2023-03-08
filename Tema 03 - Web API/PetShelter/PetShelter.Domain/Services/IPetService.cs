namespace PetShelter.Domain.Services;

public interface IPetService
{
    Task UpdatePetAsync(int petId, PetInfo petInfo);

    Task<Pet> GetPet(int petId);

    Task<IReadOnlyCollection<Pet>> GetAllPets();
    
    Task<int> RescuePetAsync(Person rescuer, Pet pet);

    Task AdoptPetAsync(Person adopter, int petId);
}
