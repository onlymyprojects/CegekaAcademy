using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public interface IFundRepository : IBaseRepository<Fund>
    {
        Task<Fund> GetFundById(int id);
    }
}
