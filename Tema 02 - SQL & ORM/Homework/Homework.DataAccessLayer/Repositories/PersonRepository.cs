using Homework.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Homework.DataAccessLayer.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {

        public PersonRepository(HomeworkContext context) : base(context)
        {
        }

        public async Task<Person?> GetPersonByIdNumber(string idNumber)
        {
            return await _context.Persons.SingleOrDefaultAsync(p => p.IdNumber == idNumber);
        }

        public async Task<Person?> GetPersonById(int? id)
        {
            return await _context.Persons.SingleOrDefaultAsync(p => p.Id == id);
        }
    }
}
