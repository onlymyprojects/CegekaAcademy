namespace BuilderPattern
{
    public class CatPetBuilder : PetBuilder
    {
        private readonly Pet _pet;

        public CatPetBuilder()
        {
            _pet = new Pet();
        }

        public void Name()
        {
            _pet.Name = "Cute cat";
        }

        public void Type()
        {
            _pet.Type = PetType.Cat;
        }

        public void BirthDate()
        {
            _pet.BirthDate = new DateTime(2026, 06, 26);
        }

        public void Description()
        {
            _pet.Description = "Very beautiful";
        }

        public void ImageUrl()
        {
            _pet.ImageUrl = "super url";
        }

        public void IsHealthy()
        {
            _pet.IsHealthy = true;
        }

        public void WeightInKg()
        {
            _pet.WeightInKg = 3;
        }

        public void Rescuer()
        {
            _pet.Rescuer = new Person("1234567890123", "Ion", new DateTime(2025, 05, 25));
        }

        public Pet GetPet()
        {
            return _pet;
        }
    }
}
