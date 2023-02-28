using Homework.DataAccessLayer.Models;

namespace Homework.DataAccessLayer.Repositories
{
    public class PetRepository : BaseRepository<Pet>, IPetRepository
    {
        public PetRepository(HomeworkContext context) : base(context)
        {
        }

        public async Task<Pet> GetPetById(int id)
        {
            return await _context.Pets.FindAsync(id);
        }

        public async Task ModifyPet(Pet pet)
        {
            var existingPet = await _context.Pets.FindAsync(pet.Id);
            if (existingPet != null)
            {
                existingPet.Name = pet.Name;
                existingPet.Description = pet.Description;
                existingPet.Type = pet.Type;
                existingPet.WeightInKg = pet.WeightInKg;
                existingPet.IsHealthy = pet.IsHealthy;
                existingPet.IsSheltered = pet.IsSheltered;

                await _context.SaveChangesAsync();
            }
            else
            {
                throw new ArgumentException($"Pet not found");
            }
        }
    }
}
