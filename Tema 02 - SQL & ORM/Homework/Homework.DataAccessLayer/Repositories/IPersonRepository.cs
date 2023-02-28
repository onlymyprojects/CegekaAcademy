using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        Task<Person?> GetPersonByIdNumber(string idNumber);
        Task<Person?> GetPersonById(int? id);
    }
}
