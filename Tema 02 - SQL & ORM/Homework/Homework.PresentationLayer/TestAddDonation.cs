using Homework.DataAccessLayer.Models;
using Homework.DataAccessLayer.Repositories;
using Homework.DataAccessLayer;

namespace Homework.PresentationLayer
{
    public class TestAddDonation
    {
        private readonly IDonationRepository donationRepository;

        public TestAddDonation()
        {
            donationRepository = new DonationRepository(new HomeworkContext());
        }

        public async Task AddDonation(Donation donation)
        {
            await donationRepository.Add(donation);
        }
    }
}
