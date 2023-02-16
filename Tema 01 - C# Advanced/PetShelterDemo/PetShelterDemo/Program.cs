//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();
var fundraiser = new Fundraiser();

Console.WriteLine("Hello, Welcome");

var exit = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a new pet", RegisterPet },
                { "Register a new fund", RegisterFund },
                { "Donate to petShelter", DonatePetShelter },
                { "Donate to fundRaiser", DonateFundRaiser },
                { "See current petShelter donations total", SeePetShelterDonations },
                { "See current fundRaiser donations total", SeeFundRaiserDonations },
                { "See our pets", SeePets },
                { "See our funds", SeeFunds },
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}


void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void RegisterFund()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");
    var target = ReadString("Target?");

    var fund = new Fund(name, description, target);

    fundraiser.RegisterFund(fund);
}

void DonatePetShelter()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    var userInput = SelectCurrency();

    Console.WriteLine("How much would you like to donate?");
    var amount = ReadInteger();

    shelter.Donate(person, userInput, amount);
}

void DonateFundRaiser()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    var userInput = SelectCurrency();

    Console.WriteLine("How much would you like to donate?");
    var amount = ReadInteger();

    fundraiser.Donate(person, userInput, amount);
}

int SelectCurrency()
{
    Console.WriteLine("Select currency:");
    Console.WriteLine("1. RON");
    Console.WriteLine("2. EUR");
    Console.WriteLine("3. USD");

    var validInput = false;
    var userInput = 0;

    while (!validInput)
    {
        userInput = Convert.ToInt32(Console.ReadLine());

        if (userInput >= 1 && userInput <= 3)
        {
            validInput = true;
        }
        else
        {
            Console.WriteLine("!!!! Invalid Input !!!!");
        }
    }

    return userInput;
}

void SeePetShelterDonations()
{
    Console.WriteLine($"Our current donation total is " +
        $"{shelter.GetTotalDonations().donationsInRon}RON, " +
        $"{shelter.GetTotalDonations().donationsInEur}EUR, " +
        $"{shelter.GetTotalDonations().donationsInUsd}USD");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeeFundRaiserDonations()
{
    Console.WriteLine($"Our current donation total is " +
        $"{fundraiser.GetTotalDonations().donationsInRon}RON, " +
        $"{fundraiser.GetTotalDonations().donationsInEur}EUR, " +
        $"{fundraiser.GetTotalDonations().donationsInUsd}USD");
    Console.WriteLine("Special thanks to our donors:");
    var donors = fundraiser.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeeFunds()
{

    var funds = fundraiser.GetAllFunds();

    var fundOptions = new Dictionary<string, Action>();
    foreach (var fund in funds)
    {
        fundOptions.Add(fund.Name, () => SeeFundDetailsByName(fund.Name));
    }

    PresentOptions("We got..", fundOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void SeeFundDetailsByName(string name)
{
    var fund = fundraiser.GetByName(name);
    Console.WriteLine($"A few words about {fund.Name}: {fund.Description}: {fund.Target}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}