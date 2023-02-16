using PetShelterDemo.DAL;

namespace PetShelterDemo.Domain
{
    public class Fundraiser
    {
        private readonly IRegistry<Fund> fundRegistry;
        private readonly IRegistry<Person> donorRegistry;
        private Donation donation;

        public Fundraiser()
        {
            fundRegistry = new Registry<Fund>(new Database());
            donorRegistry = new Registry<Person>(new Database());
            donation = new Donation();
        }

        public void RegisterFund(Fund fund)
        {
            fundRegistry.Register(fund);
        }

        public IReadOnlyList<Fund> GetAllFunds()
        {
            return fundRegistry.GetAll().Result;
        }

        public Fund GetByName(string name)
        {
            return fundRegistry.GetByName(name).Result;
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
}
