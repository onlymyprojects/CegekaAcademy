using Homework.Common.Enums;
using Homework.DataAccessLayer.Models;
using HomeWork.Domain.Services;
using System;

var petShelter = new PetShelter();
var fundRaiser = new FundRaiser();

Console.WriteLine("\nHello, Welcome!\n");

var exit = false;

try
{
    while (!exit)
    {
        var menu = new Dictionary<string, Func<Task>>(){
            {"Register a new pet", RegisterPet},
            {"See our pets", SeePets},
            {"Adopt a pet", AdoptPet},
            {"Donate to pet shelter", () => DonateMoney(1)},
            {"See current pet shelter donations", () => SeeDonations(DonationDestination.PetShelter)},
            {"Register a new fund", RegisterFund},
            {"See our funds", SeeFunds},
            {"Donate to fund raiser", () => DonateMoney(2)},
            {"See current fund raiser donations", () => SeeDonations(DonationDestination.FundRaiser)},
            { "Leave:(", Leave }
        };

        Console.WriteLine("\nHere's what you can do...");

        for (int i = 0; i < menu.Count; i++)
        {
            Console.WriteLine(i + 1 + ". " + menu.ElementAt(i).Key);
        }

        var userInput = IntInputValidator(menu.Count);

        try
        {
            await menu.ElementAt(userInput - 1).Value();
        }
        catch (Exception ex)
        {

            Console.WriteLine($"An error occurred while calling {userInput} method from menu: " + ex.Message);
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
}

async Task RegisterPet()
{
    var pet = new Pet();

    Console.WriteLine("\nName ?");
    pet.Name = Console.ReadLine();

    Console.WriteLine("\nDescription ?");
    pet.Description = Console.ReadLine();

    Console.WriteLine("\nType ?");
    for (int i = 0; i < Enum.GetNames(typeof(PetType)).Length; i++)
    {
        Console.WriteLine($"{i + 1}. {(PetType)i}");
    }
    pet.Type = (PetType)IntInputValidator(Enum.GetNames(typeof(PetType)).Length) - 1;

    Console.WriteLine("\nWeight in kg ?");
    pet.WeightInKg = Convert.ToDecimal(Console.ReadLine());

    Console.WriteLine("\nIs healthy ?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");
    pet.IsHealthy = (IntInputValidator(2) == 1) ? true : false;

    try
    {
        await SetPerson(pet, 1, null, null);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling SetPerson() form RegisterPet(): " + ex.Message);
    }

    pet.IsSheltered = true;

    try
    {
        await petShelter.AddPets(pet);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling AddPets() from RegisterPet(): " + ex.Message);
    }
}

async Task RegisterFund()
{
    var fund = new Fund();

    Console.WriteLine("\nName ?");
    fund.Name = Console.ReadLine();

    Console.WriteLine("\nDescription ?");
    fund.Description = Console.ReadLine();

    Console.WriteLine("\nTarget ?");
    fund.Target = Console.ReadLine();

    try
    {
        await SetPerson(null, null, null, fund);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling SetPerson() from RegisterFund(): " + ex.Message);
    }

    try
    {
        await fundRaiser.AddFunds(fund);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling AddFunds() from RegisterFund(): " + ex.Message);
    }
}

async Task SetPerson(Pet? pet, int? type, Donation? petShelterDonation, Fund? fund)
{
    Console.WriteLine("\nHave you ever registered/adopted pets/funds, or are you here for the first time?");
    Console.WriteLine("1. Yes, I have already registered/adopted animals");
    Console.WriteLine("2. No, I am here for the first time");
    var newPerson = IntInputValidator(2);
    Person? person = null;

    var setPerson = false;
    while (!setPerson)
    {
        if (newPerson == 1)
        {
            for (; ; )
            {
                Console.WriteLine("\nPlease introduce your Id number:");
                var IdNumber = Console.ReadLine();
                
                try
                {
                    person = await petShelter.PersonByIdNumber(IdNumber);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred while calling PersonByIdNumber() from SetPerson(): " + ex.Message);
                }

                if (person == null)
                {
                    Console.WriteLine($"\nPerson with ID number {IdNumber} was not found.");
                    Console.WriteLine("1. Try again ?");
                    Console.WriteLine("2. Register new person ?");
                    var invalidIdNumber = IntInputValidator(2);
                    if (invalidIdNumber == 2)
                    {
                        newPerson = 2;
                        break;
                    }
                }
                else
                {
                    if (pet != null)
                    {
                        if (type == 1)
                        {
                            pet.RescuerId = person.Id;
                        }
                        else if (type == 2)
                        {
                            pet.AdopterId = person.Id;
                        }
                    }
                    else if (petShelterDonation != null)
                    {
                        petShelterDonation.DonorId = person.Id;
                    }
                    else if (fund != null)
                    {
                        fund.OwnerId = person.Id;
                    }

                    setPerson = true;

                    break;
                }
            }
        }
        else
        {
            person = NewPerson();

            if (pet != null)
            {
                if (type == 1)
                {
                    pet.Rescuer = person;
                }
                else if (type == 2)
                {
                    pet.Adopter = person;
                }
            }
            else if (petShelterDonation != null)
            {
                petShelterDonation.Donor = person;
            }
            else if (fund != null)
            {
                fund.Owner = person;
            }

            setPerson = true;
        }
    }
}

Person? NewPerson()
{
    var person = new Person();

    Console.WriteLine("\nDo you want to be registered ?");
    Console.WriteLine("1. Yes");
    Console.WriteLine("2. No");
    var resgister = IntInputValidator(2);

    if (resgister == 1)
    {
        Console.WriteLine("\nWhat is your name ?");
        person.Name = Console.ReadLine();

        Console.WriteLine("\nWhat is your date of birth ? (MM/DD/YYYY)");
        for (; ; )
        {
            if (DateTime.TryParse(Console.ReadLine(), out DateTime convertedDateOfBirth))
            {
                person.DateOfBirth = convertedDateOfBirth;
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter your date of birth in MM/DD/YYYY format.");
            }
        }

        Console.WriteLine("\nWhat is your id number ?");
        person.IdNumber = Console.ReadLine();
    }
    else
    {
        person = null;
    }

    return person;
}

async Task SeePets()
{
    var pets = new List<Pet>();

    try
    {
        pets = await petShelter.GetAllPets();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling GetAllPets() from SeePets(): " + ex.Message);
    }

    if (pets.Count != 0)
    {
        var i = 1;

        Console.WriteLine();

        foreach (var pet in pets)
        {
            Console.WriteLine($"{i}. {pet.Name}");
            i++;
        }

        var chosenPet = IntInputValidator(pets.Count);

        Console.WriteLine($"\nName: {pets[chosenPet - 1].Name}");
        Console.WriteLine($"Description: {pets[chosenPet - 1].Description}");
        Console.WriteLine($"Type: {pets[chosenPet - 1].Type}");
        Console.WriteLine($"Weight in kg: {pets[chosenPet - 1].WeightInKg}");
        Console.WriteLine($"Is healthy: {pets[chosenPet - 1].IsHealthy}");

        var rescuer = new Person();
        var adopter = new Person();

        try
        {
            rescuer = await petShelter.PersonById(pets[chosenPet - 1].RescuerId);
            adopter = await petShelter.PersonById(pets[chosenPet - 1].AdopterId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while calling PersonById() from SeePets(): " + ex.Message);
        }

        SeePersonInfo(rescuer, 1);
        SeePersonInfo(adopter, 2);
    }
    else
    {
        Console.WriteLine("\nThere is no pets in database.");
    }
}

async Task SeeFunds()
{
    var funds = new List<Fund>();

    try
    {
        funds = await fundRaiser.GetAllFunds();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling GetAllFunds() from SeeFunds(): " + ex.Message);
    }

    if (funds.Count != 0)
    {
        var i = 1;

        Console.WriteLine();

        foreach (var fund in funds)
        {
            Console.WriteLine($"{i}. {fund.Name}");
            i++;
        }

        var chosenFund = IntInputValidator(funds.Count);

        Console.WriteLine($"\nName: {funds[chosenFund - 1].Name}");
        Console.WriteLine($"Description: {funds[chosenFund - 1].Description}");
        Console.WriteLine($"Type: {funds[chosenFund - 1].Target}");

        var owner = new Person();

        try
        {
            owner = await fundRaiser.PersonById(funds[chosenFund - 1].OwnerId);
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while calling PersonById() from SeeFunds(): " + ex.Message);
        }

        SeePersonInfo(owner, 4);
    }
    else
    {
        Console.WriteLine("\nThere is no funds in database.");
    }
}

void SeePersonInfo(Person? person, int type)
{
    string personType = string.Empty;

    if (type == 1)
    {
        personType = "Rescuer";
    }
    else if (type == 2)
    {
        personType = "Adopter";
    }
    else if (type == 3)
    {
        personType = "Donor";
    }
    else if (type == 4)
    {
        personType = "Owner";
    }

    if (person != null)
    {
        Console.WriteLine($"\n{personType} name: {person.Name}");
        Console.WriteLine($"{personType} date of birth: {person.DateOfBirth.Date}");
        Console.WriteLine($"{personType} Id number: {person.IdNumber}");
    }
    else
    {
        Console.WriteLine($"\n{personType}: no information");
    }
}

async Task AdoptPet()
{
    for (; ; )
    {
        Console.WriteLine("\n1. See sheltered pets");
        Console.WriteLine("2. If you have chosen the desired pet, adopt it!");
        Console.WriteLine("3. You changed your mind, and don't want to adopt a pet anymore");

        var userInput = IntInputValidator(3);

        var pets = new List<Pet>();

        try
        {
            pets = await petShelter.GetAllPets();
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred while calling GetAllPets() from AdoptPet(): " + ex.Message);
        }

        if (userInput == 1)
        {
            Console.WriteLine();

            if (pets.Count != 0)
            {
                var shelteredPets = new Dictionary<string, int>();
                foreach (var pet in pets)
                {
                    if (pet.IsSheltered == true)
                    {
                        shelteredPets.Add(pet.Name, pet.Id);
                    }
                }

                if (shelteredPets.Count != 0)
                {
                    for (int i = 0; i < shelteredPets.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + shelteredPets.ElementAt(i).Key);
                    }

                    var chosenPet = IntInputValidator(shelteredPets.Count);

                    var petToBeShown = new Pet();

                    try
                    {
                        petToBeShown = await petShelter.PetById(shelteredPets.ElementAt(chosenPet - 1).Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while calling PetById() from AdoptPet(): " + ex.Message);
                    }

                    Console.WriteLine($"\nName: {petToBeShown.Name}");
                    Console.WriteLine($"Description: {petToBeShown.Description}");
                    Console.WriteLine($"Type: {petToBeShown.Type}");
                    Console.WriteLine($"Weight in kg: {petToBeShown.WeightInKg}");
                    Console.WriteLine($"Is healthy: {petToBeShown.IsHealthy}");

                    var rescuer = new Person();

                    try
                    {
                        rescuer = await petShelter.PersonById(petToBeShown.RescuerId);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while calling PersonById() from AdoptPet(): " + ex.Message);
                    }

                    SeePersonInfo(rescuer, 1);
                }
                else
                {
                    Console.WriteLine("\nThere is no sheltered pets in database.");
                }
            }
            else
            {
                Console.WriteLine("\nThere is no pets in database.");
            }
        }
        else if (userInput == 2)
        {
            Console.WriteLine("\nJust choose the desired pet:");

            if (pets.Count != 0)
            {
                var shelteredPets = new Dictionary<string, int>();
                foreach (var pet in pets)
                {
                    if (pet.IsSheltered == true)
                    {
                        shelteredPets.Add(pet.Name, pet.Id);
                    }
                }

                if (shelteredPets.Count != 0)
                {
                    for (int i = 0; i < shelteredPets.Count; i++)
                    {
                        Console.WriteLine(i + 1 + ". " + shelteredPets.ElementAt(i).Key);
                    }

                    var chosenPet = IntInputValidator(shelteredPets.Count);

                    var petToBeAdopdet = new Pet();

                    try
                    {
                        petToBeAdopdet = await petShelter.PetById(shelteredPets.ElementAt(chosenPet - 1).Value);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while calling PetById() from AdoptPet(): " + ex.Message);
                    }

                    try
                    {
                        await SetPerson(petToBeAdopdet, 2, null, null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while calling SetPerson() from AdoptPet(): " + ex.Message);
                    }

                    petToBeAdopdet.IsSheltered = false;

                    try
                    {
                        await petShelter.ModifyExistingPet(petToBeAdopdet);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while calling ModifyExistingPet() from AdoptPet(): " + ex.Message);
                    }

                    Console.WriteLine("\nPet was adopted, thank you!!!");
                }
                else
                {
                    Console.WriteLine("\nThere is no sheltered pets in database.");
                }
            }
            else
            {
                Console.WriteLine("\nThere is no pets in database.");
            }

            break;
        }
        else if (userInput == 3)
        {
            Console.WriteLine("\nOk, hope to see you again :)");
            break;
        }
    }
}

async Task DonateMoney(int type)
{
    var donation = new Donation();

    Console.WriteLine("\nName of donation ?");
    donation.Name = Console.ReadLine();

    Console.WriteLine("\nCurrency ?");
    for (int i = 0; i < Enum.GetNames(typeof(Currencies)).Length; i++)
    {
        Console.WriteLine($"{i + 1}. {(Currencies)i}");
    }
    donation.Currency = (Currencies)IntInputValidator(Enum.GetNames(typeof(Currencies)).Length) - 1;

    Console.WriteLine("\nAmount ?");
    donation.Amount = Convert.ToDecimal(Console.ReadLine());

    if (type == 1)
    {
        donation.Destination = DonationDestination.PetShelter;
    }
    else if (type == 2)
    {
        donation.Destination = DonationDestination.FundRaiser;
    }

    try
    {
        await SetPerson(null, null, donation, null);
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling SetPerson() from DonateMoney(): " + ex.Message);
    }

    try
    {
        if (type == 1)
        {
            await petShelter.Donate(donation);
        }
        else if (type == 2)
        {
            await fundRaiser.Donate(donation);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling Donate() from DonateMoney(): " + ex.Message);
    }

    Console.WriteLine("\nDonation complete, thank you!!!");
}

async Task SeeDonations(DonationDestination destination) {
    var donations = new List<Donation>();

    try
    {
        donations = await petShelter.GetAllDonations();
    }
    catch (Exception ex)
    {
        Console.WriteLine("An error occurred while calling GetAllDonations() from SeePetShelterDonations(): " + ex.Message);
    }

    var j = 1;
    for (int i = 0; i < donations.Count; i++)
    {
        if (donations[i].Destination == destination)
        {
            Console.WriteLine($"\n---- Donation nr. {j} ----");
            Console.WriteLine($"Donation name: {donations[i].Name}");
            Console.WriteLine($"Donation currency: {donations[i].Currency}");
            Console.WriteLine($"Donation amount: {donations[i].Amount}");

            var donor = new Person();

            try
            {
                if (destination == DonationDestination.PetShelter)
                {
                    donor = await petShelter.PersonById(donations[i].DonorId);
                }
                else if (destination == DonationDestination.FundRaiser)
                {
                    donor = await fundRaiser.PersonById(donations[i].DonorId);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while calling PersonById() from SeeDonations(): " + ex.Message);
            }
            SeePersonInfo(donor, 3);

            j++;
        }
    }
}

async Task Leave()
{
    Console.WriteLine("\nGood bye!\n");
    exit = true;
}

int IntInputValidator(int maxLength)
{
    var validInput = false;
    var userInput = 0;

    while (!validInput)
    {
        userInput = Convert.ToInt32(Console.ReadLine());
        if (userInput >= 1 && userInput <= maxLength)
        {
            validInput = true;
        }
        else
        {
            Console.WriteLine("\n!!!! Invalid input !!!!\n");
        }
    }

    return userInput;
}