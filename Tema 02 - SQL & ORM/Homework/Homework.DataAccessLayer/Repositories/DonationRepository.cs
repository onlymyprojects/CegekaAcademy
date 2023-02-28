using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public class DonationRepository : BaseRepository<Donation>, IDonationRepository
    {
        public DonationRepository(HomeworkContext context) : base(context)
        {
        }
    }
}
