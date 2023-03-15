namespace BuilderPattern
{
    public class PetCreation
    {
        private readonly PetBuilder _petBuilder;

        public PetCreation(PetBuilder petBuilder)
        {
            _petBuilder = petBuilder;
        }

        public Pet CreatePet()
        {
            _petBuilder.Name();
            _petBuilder.Type();
            _petBuilder.BirthDate();
            _petBuilder.Description();
            _petBuilder.ImageUrl();
            _petBuilder.IsHealthy();
            _petBuilder.WeightInKg();
            _petBuilder.Rescuer();

            return _petBuilder.GetPet();
        }

        public void PrintPetDetails()
        {
            var pet = _petBuilder.GetPet();
            Console.WriteLine($"\nName: {pet.Name}");
            Console.WriteLine($"Type: {pet.Type}");
            Console.WriteLine($"Birth Date: {pet.BirthDate}");
            Console.WriteLine($"Description: {pet.Description}");
            Console.WriteLine($"Image Url: {pet.ImageUrl}");
            Console.WriteLine($"Is Healty: {pet.IsHealthy}");
            Console.WriteLine($"Weight In Kg: {pet.WeightInKg}");
            Console.WriteLine($"Rescuer Name: {pet.Rescuer.Name}");
        }
    }
}
