using Homework.DataAccessLayer;
using Homework.DataAccessLayer.Models;
using Homework.DataAccessLayer.Repositories;

namespace Homework.PresentationLayer
{
    public class TestAddPerson
    {
        private readonly IPersonRepository personRepository;

        public TestAddPerson()
        {
            personRepository = new PersonRepository(new HomeworkContext());
        }

        public void AddPerson(Person person)
        {
            personRepository.Add(person);
        }
    }
}
