using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public interface IPetRepository : IBaseRepository<Pet>
    {
        Task<Pet> GetPetById(int id);
        Task ModifyPet(Pet pet);
    }
}
