using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public class FundRepository : BaseRepository<Fund>, IFundRepository
    {
        public FundRepository(HomeworkContext context) : base(context)
        {
        }

        public async Task<Fund> GetFundById(int id)
        {
            return await _context.Funds.FindAsync(id);
        }
    }
}
