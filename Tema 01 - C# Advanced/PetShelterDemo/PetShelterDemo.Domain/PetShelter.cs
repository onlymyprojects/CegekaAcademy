using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain;

public class PetShelter
{
    private readonly IRegistry<Pet> petRegistry;
    private readonly IRegistry<Person> donorRegistry;
    private Donation donation;

    public PetShelter()
    {
        donorRegistry = new Registry<Person>(new Database());
        petRegistry = new Registry<Pet>(new Database());
        donation = new Donation();
    }

    public void RegisterPet(Pet pet)
    {
        petRegistry.Register(pet);
    }

    public IReadOnlyList<Pet> GetAllPets()
    {
        return petRegistry.GetAll().Result; // Actually blocks thread until the result is available.
    }

    public Pet GetByName(string name)
    {
        return petRegistry.GetByName(name).Result;
    }

    public void Donate(Person donor, int currency, int amount)
    {
        var donors = donorRegistry.GetAll().Result;
        var newDonor = true;
        // verify by id if this user is already in our database
        foreach (var oneDonor in donors)
        {
            if (oneDonor.IdNumber == donor.IdNumber)
            {
                newDonor = false;
            }
        }

        if (newDonor)
        {
            donorRegistry.Register(donor);
        }

        if (currency == 1) // RON
        {
            donation.donationsInRon += amount;
        }
        else if (currency == 2) // EUR
        {
            donation.donationsInEur += amount;
        }
        else // USD
        {
            donation.donationsInUsd += amount;
        }
    }

    public Donation GetTotalDonations()
    {
        return donation;
    }

    public IReadOnlyList<Person> GetAllDonors()
    {
        return donorRegistry.GetAll().Result;
    }
}