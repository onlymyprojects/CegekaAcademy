using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository
{
    public interface IFundRepository : IBaseRepository<Fund>
    {
        Task<Fund> GetFundById(int id);
    }
}
