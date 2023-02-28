namespace Homework.DataAccessLayer.Models
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string IdNumber { get; set; }

        public ICollection<Pet> RescuedPets { get; set; }
        public ICollection<Pet> AdoptedPets { get; set; }
        public ICollection<Donation> Donations { get; set; }
        public ICollection<Fund> Funds { get; set; }
    }
}
