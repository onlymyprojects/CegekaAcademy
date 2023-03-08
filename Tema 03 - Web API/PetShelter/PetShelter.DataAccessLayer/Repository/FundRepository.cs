using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundRepository : BaseRepository<Fund>, IFundRepository
    {
        public FundRepository(PetShelterContext context) : base(context)
        {
        }

        public async Task<Fund> GetFundById(int id)
        {
            return await _context.Funds.FindAsync(id);
        }
    }
}
